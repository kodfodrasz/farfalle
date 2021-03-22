module Farfalle.Program

open System
open Falco
open Falco.Routing
open Falco.HostBuilder
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection

open Farfalle.Pages
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.FileProviders
open System.IO
open Microsoft.AspNetCore.Http.Features
open Microsoft.AspNetCore.Server.Kestrel.Core

// ------------
// Register services
// ------------
let configureServices (services: IServiceCollection) =
  services
    .AddFalco()
    .Configure(fun (o:KestrelServerOptions) -> o.Limits.MaxRequestBodySize <- Nullable())
    .Configure(fun (o: FormOptions) ->
      o.ValueCountLimit <- 255
      o.MultipartBodyLengthLimit <- Int64.MaxValue
      o.MultipartBoundaryLengthLimit <- 2 * 1024 * 1024 * 1024) // 2GB
  |> ignore

// ------------
// Activate middleware
// ------------
let configureApp
  (config: Config)
  (endpoints: HttpEndpoint list)
  (ctx: WebHostBuilderContext)
  (app: IApplicationBuilder)
  =
  let devMode =
    StringUtils.strEquals ctx.HostingEnvironment.EnvironmentName "Development"

  let uploadsDir = Path.Combine(config.DataDir, "uploads")
  Directory.CreateDirectory uploadsDir |> ignore

  app
    .UseWhen(devMode, (fun app -> app.UseDeveloperExceptionPage()))
    .UseWhen(
      not (devMode),
      fun app ->
        app.UseFalcoExceptionHandler(
          Response.withStatusCode 500
          >> Response.ofPlainText "Server error"
        )
    )
    .UseFalco(endpoints)
    .UseStaticFiles() // wwwroot
    .UseStaticFiles(
      StaticFileOptions(RequestPath = (PathString) "/uploads", FileProvider = new PhysicalFileProvider(uploadsDir))
    )
  |> ignore


// -----------
// Configure Web host
// -----------
let configureWebHost config (endpoints: HttpEndpoint list) (webHost: IWebHostBuilder) =
  webHost
    .ConfigureServices(configureServices)
    .Configure(configureApp config endpoints)

[<EntryPoint>]
let main args =
  let config = Config.FromEnv() |> Option.get

  webHost args {
    configure (configureWebHost config)

    endpoints [ get "/" (Request.mapQuery HomePage.parseHomeHandlerParams HomePage.homeHandler)
                post "/upload-file" (UploadFile.uploadFileHandler config) ]
  }

  0
