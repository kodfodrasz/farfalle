[<AutoOpen>]
[<RequireQualifiedAccess>]
module Farfalle.Pages.UploadFile

open Farfalle

open System
open System.IO
open System.Threading.Tasks
open System.Security.Cryptography
open Microsoft.AspNetCore.Http



open Falco
open Falco.Multipart
open FSharp.Control.Tasks.V2

let sha256 (stream: Stream) =
  use algo = SHA256.Create()
  let hash = algo.ComputeHash stream
  let str = BitConverter.ToString hash
  str.Replace("-", "").ToLowerInvariant()


let getFileUrl (config: Config) (filePath: string) : string =
  let relativePath =
    Path.GetRelativePath(config.DataDir, filePath)

  if Path.DirectorySeparatorChar <> '/' then
    relativePath.Replace(Path.DirectorySeparatorChar, '/')
  else
    relativePath

let saveFile config filesDir (formFile: IFormFile) =
  let tempfileName = Path.GetTempFileName()

  do
    ()
    use tempfileWriter = File.OpenWrite tempfileName
    formFile.CopyTo tempfileWriter
    tempfileWriter.Flush()

  let fileHash =
    use tempReadStream = File.OpenRead tempfileName
    sha256 tempReadStream

  let fileExtension = Path.GetExtension formFile.FileName

  let filePathWithoutExtension = Path.Combine(filesDir, fileHash)

  let filePath =
    Path.ChangeExtension(filePathWithoutExtension, fileExtension)

  if File.Exists filePath
  then File.Delete tempfileName
  else File.Move(tempfileName, filePath)

  getFileUrl config filePath



let uploadFileHandler (config: Config) (context: HttpContext) : Task =
  task {
    let uploadsDir = Path.Combine(config.DataDir, "uploads")
    Directory.CreateDirectory uploadsDir |> ignore

    let! form = context.Request.TryStreamFormAsync()

    let uploadResult =
      form
      |> Result.map
           (fun rd ->
             rd.Files
             |> Option.map (Seq.map (saveFile config uploadsDir))
             |> Option.defaultValue Seq.empty<string>
             |> Seq.toList)

    match uploadResult with
    | Error e -> return! Response.redirect "/" false context
    | Ok [] -> return! Response.redirect "/" false context
    | Ok (first :: rest) -> return! Response.redirect first false context

  }
