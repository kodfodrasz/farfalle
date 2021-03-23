module App

open Browser.Dom
open Browser.Types

//
// DOM nodes of interest
//

// Drop area
let dropArea = document.querySelector ("#drop-zone") :?> Browser.Types.HTMLButtonElement

// Forms
let inputFiles = document.querySelector("#upload-form-input-file") :?> HTMLInputElement
let inputSubmit = document.querySelector("#upload-form-input-submit") :?> HTMLInputElement

//
// Event handling code
//

// Paste event
let pasteEventHandler (ce:ClipboardEvent) = 
    let logger msg = console.log (sprintf "paste %s" msg)
    logger "event received"

    let dt = ce.clipboardData

    for i in [ 0 .. (dt.items.length - 1) ] do
      let item = dt.items.[i]
      logger (sprintf "item[%i] kind: %s type: %s" i item.kind item.``type``)

    for i in [ 0 .. (dt.files.length - 1) ] do
      let file = dt.files.[i]
      logger (sprintf "file[%i] name: %s size: %i type: %s" i file.name file.size file.``type``)

    inputFiles.files <- dt.files
    inputSubmit.click()

// Drop event
let dragEventHandler (de:DragEvent) =
    de.preventDefault()

let dropEventHandler (de:DragEvent) =
    de.preventDefault()

    let logger msg = console.log (sprintf "drop %s" msg)
    logger "event received"

    let dt = de.dataTransfer

    for i in [ 0 .. (dt.items.length - 1) ] do
      let item = dt.items.[i]
      logger (sprintf "item[%i] kind: %s type: %s" i item.kind item.``type``)

    for i in [ 0 .. (dt.files.length - 1) ] do
      let file = dt.files.[i]
      logger (sprintf "file[%i] name: %s size: %i type: %s" i file.name file.size file.``type``)

    inputFiles.files <- dt.files
    inputSubmit.click()


let clickEventHandler (me:MouseEvent) =
  inputFiles.click()

let submitFiles (e:Event) = 
  inputSubmit.click()

//
// Event handling registrations
//

document.onpaste <- pasteEventHandler

dropArea.ondragover <- dragEventHandler
dropArea.ondrop <- dropEventHandler
dropArea.onclick <- clickEventHandler

inputFiles.onchange <- submitFiles
