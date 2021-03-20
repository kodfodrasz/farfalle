[<AutoOpen>]
[<RequireQualifiedAccess>]
module Farfalle.Pages.PageFrame

open Falco.Markup
open Falco.Markup.Elem

let copyrightSinceYear = 2021
let currentYear = System.DateTime.Now.Year

let private faviconHeadMeta =
  let appleIconSizes =
    [ 57
      60
      72
      76
      114
      120
      144
      152
      180 ]

  let androidIconSizes = [ 192 ]
  let pngFaviconSizes = [ 16; 32; 96 ]
  let msApplicationSizes = [ 144 ]
  let tilecolor = "#777777"

  let appleTouchIcon size =
    link [ Attr.rel "apple-touch-icon"
           Attr.create "sizes" (sprintf "%i" size)
           Attr.href (sprintf "/favicons/apple-icon-%ix%i.png" size size) ]

  let androidIcon size =
    link [ Attr.rel "icon"
           Attr.type' "image/png"
           Attr.create "sizes" (sprintf "%i" size)
           Attr.href (sprintf "/favicons/android-icon-%ix%i.png" size size) ]

  let pngFavicon size =
    link [ Attr.rel "icon"
           Attr.type' "image/png"
           Attr.create "sizes" (sprintf "%i" size)
           Attr.href (sprintf "/favicons/favicon-%ix%i.png" size size) ]

  let pngFavicon size =
    link [ Attr.rel "icon"
           Attr.type' "image/png"
           Attr.create "sizes" (sprintf "%i" size)
           Attr.href (sprintf "/favicons/favicon-%ix%i.png" size size) ]

  let msapplicationTileImage size =
    meta [ Attr.name "msapplication-TileImage"
           Attr.content (sprintf "/favicons/ms-icon-%ix%i.png" size size) ]


  let manifest =
    link [ Attr.rel "manifest"
           Attr.href "/manifest.json" ]

  let msapplicationTileColor =
    meta [ Attr.name "msapplication-TileColor"
           Attr.content tilecolor ]

  let themeColor =
    meta [ Attr.name "theme-color"
           Attr.content tilecolor ]

  seq {
    yield! Seq.map appleTouchIcon appleIconSizes
    yield! Seq.map androidIcon androidIconSizes
    yield! Seq.map pngFavicon pngFaviconSizes

    yield msapplicationTileColor
    yield! Seq.map msapplicationTileImage msApplicationSizes

    yield themeColor

    yield manifest
  }
  |> Seq.toList

let renderPageFrame title' content =
  html [ Attr.lang "en" ] [
    head
      []
      (List.collect
        id
        [ [ meta [ Attr.charset "utf-8" ]
            link [ Attr.rel "stylesheet"
                   Attr.href "/css/farfalle.css" ]

            Elem.title [] [
              if System.String.IsNullOrWhiteSpace title' then
                Text.raw "Farfalle"
              else
                Text.rawf "%s - Farfalle" title'
            ] ]
          faviconHeadMeta ])
    body [] [
      div [ Attr.class' "header" ] [
        img [ Attr.src "/images/farfalle-256px.png"
              Attr.class' "brand-logo" ]
        div [ Attr.class' "brand-name" ] [
          Text.raw "Farfalle"
        ]
      ]
      div [ Attr.class' "main" ] content
      div [ Attr.class' "footer" ] [
        div [ Attr.class' "brand-copyright" ] [
          if copyrightSinceYear = currentYear then
            Text.rawf "Farfalle © %i" copyrightSinceYear
          else
            Text.rawf "Farfalle © %i&nbsp;&emdash;&nbsp;%i" copyrightSinceYear currentYear
        ]
      ]
      script [ Attr.src "/js/farfalle.js" ] []
    ]
  ]
