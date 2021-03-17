namespace Farfalle

open System
open System.IO

type Config =
  {
    DataDir : string
  }
  with
  static member FromEnv() = 
    let ddMaybe = Environment.GetEnvironmentVariable "FARFALLE_DATA_DIR" |> Option.ofObj

    match ddMaybe with
    | Some dd when Directory.Exists dd -> 
      { DataDir = Path.GetFullPath dd } |> Some
    | _ -> None

