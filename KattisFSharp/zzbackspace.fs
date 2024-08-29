module KattisFSharp.zzbackspace

open System
open KattisFSharp.aaKattio

let rec filterBackspace (input: char list) acc : string =
    match input, acc with
    | '<' :: tail, _ :: acc' -> filterBackspace tail acc'
    | c :: tail, _ -> filterBackspace tail (c::acc)
    | [], acc' -> System.String(List.rev acc' |> Array.ofList)

(*[<EntryPoint>]
let main argv =
    let scanner = Scanner()
    let s = scanner.Next()
    use stdout = new BufferedStdoutWriter()
    let stringAsArray = Seq.toList s
    stdout.WriteLine(filterBackspace stringAsArray [])
    stdout.Flush
    0*)
