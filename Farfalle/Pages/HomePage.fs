[<AutoOpen>]
[<RequireQualifiedAccess>]
module Farfalle.Pages.HomePage

open System.Threading.Tasks
open Microsoft.AspNetCore.Http

open Falco
open Falco.Markup
open Falco.Markup.Elem

let homeHandler : HttpContext -> Task =
  html [] [
    head [] []
    body [] [
      h1 [] [ Text.raw "Farfalle" ]
      img [ Attr.src "/images/farfalle-512px.png" ]
      p [] [ Text.raw "Welcome!" ]
      p [] [ Text.raw "Upload a file!" ]
      form [ Attr.action "/upload-file"
             Attr.method "POST"
             Attr.enctype "multipart/form-data" ] [
        input [ Attr.type' "file"
                Attr.id "file-input"
                Attr.name "filename" ]
        input [ Attr.type' "submit" ]
      ]
    ]
  ]


  |> Response.ofHtml
