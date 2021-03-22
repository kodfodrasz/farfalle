module App

open Browser.Dom
open Browser.Types


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


let forwardClick (target : HTMLElement) (me:MouseEvent) = 
  target.click()

// Drop area
let dropArea = document.querySelector ("#drop-zone") :?> Browser.Types.HTMLButtonElement

// Forms
let inputFiles = document.querySelector("#upload-form-input-file") :?> HTMLInputElement
let inputSubmit = document.querySelector("#upload-form-input-submit") :?> HTMLInputElement


document.onpaste <- pasteEventHandler

dropArea.ondragover <- dragEventHandler
dropArea.ondrop <- dropEventHandler
dropArea.onclick <- forwardClick inputFiles
