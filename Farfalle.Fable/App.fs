module App

open Browser.Dom
open Browser.Types


// Mutable variable to count the number of times we clicked the button
let mutable count = 0

// Get a reference to our button and cast the Element to an HTMLButtonElement
let myButton =
  document.querySelector ("#my-button") :?> Browser.Types.HTMLButtonElement

// Register our listener
myButton.onclick <-
  fun _ ->
    count <- count + 1
    myButton.innerText <- sprintf "You clicked: %i time(s)" count

// Paste event
document.body.onpaste <-
  fun e ->
    let logger msg = console.log (sprintf "paste %s" msg)
    logger "event received"

    let ce = unbox<ClipboardEvent> e

    let dt = ce.clipboardData

    for i in [ 0 .. (dt.items.length - 1) ] do
      let item = dt.items.[i]
      logger (sprintf "item[%i] kind: %s type: %s" i item.kind item.``type``)

    for i in [ 0 .. (dt.files.length - 1) ] do
      let file = dt.files.[i]
      logger (sprintf "file[%i] name: %s size: %i type: %s" i file.name file.size file.``type``)

// Drop event
let dropArea =
  document.querySelector ("#drop-zone") :?> Browser.Types.HTMLDivElement


dropArea.ondragover <-
  fun de -> 
    de.preventDefault()

dropArea.ondrop <-
  fun de ->
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

