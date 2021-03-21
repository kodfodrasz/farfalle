[<AutoOpen>]
[<RequireQualifiedAccess>]
module Farfalle.Pages.HomePage

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Http

open Falco
open Falco.Markup
open Falco.Markup.Elem

type HomeParams = { Lorem: int }

let parseHomeHandlerParams (query: QueryCollectionReader) = { Lorem = query.GetInt "lorem" 0 }

let homeHandler param =
  let lipsum =
    let gen = NLipsum.Core.LipsumGenerator()

    param.Lorem
    |> gen.GenerateParagraphs
    |> String.concat "\n<br/><br/>\n"

  PageFrame.renderPageFrame
    String.Empty
    [ h1 [] [
        Text.raw "Welcome to Farfalle!"
      ]
      p [] [
        Text.raw
          "What is Farfalle? Farfalle is a copy-pasta application to quickly share self-deleting ephemeral text snippets, images, or files with others. Oh, and also it is the name of a kind of italian pasta."
      ]
      h2 [] [
        Text.raw "Go ahead: upload a file!"
      ]
      p [] [
        Text.raw "Use the form below to upload a file!"
      ]
      form [ Attr.class' "upload-form"
             Attr.action "/upload-file"
             Attr.method "POST"
             Attr.enctype "multipart/form-data" ] [
        input [ Attr.class' "upload-form-file"
                Attr.type' "file"
                Attr.id "file-input"
                Attr.name "filename" ]
        input [ Attr.class' "form"
                Attr.type' "submit" ]
      ]
      p [ Attr.class' "lorem" ] [
        Text.raw lipsum
      ]
      div [ Attr.id "test" ] [
        button [ Attr.id "my-button" ] [
          Text.raw "Click me"
        ]

        div [ Attr.class' "drop-zone" ] [
          div [ Attr.class' "drop-zone-container" ] [
            img [ Attr.src "images/upload-file.png" ]
            span [] [ Text.raw "Drop files here!" ]
          ]
          div [ Attr.id "drop-zone" ] []
        ]
      ] ]
  |> Response.ofHtml
