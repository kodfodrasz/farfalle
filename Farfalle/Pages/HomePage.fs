[<AutoOpen>]
[<RequireQualifiedAccess>]
module Farfalle.Pages.HomePage

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Http

open Falco
open Falco.Markup
open Falco.Markup.Elem

let homeHandler : HttpContext -> Task =
  PageFrame.renderPageFrame
    String.Empty
    [
      h1 [] [ Text.raw "Welcome to Farfalle!" ]
      p [] [ Text.raw "What is Farfalle? Farfalle is a copy-pasta application to quickly share self-deleting ephemeral text snippets, images, or files with others. Oh, and also it is the name of a kind of italian pasta."]
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
  |> Response.ofHtml
