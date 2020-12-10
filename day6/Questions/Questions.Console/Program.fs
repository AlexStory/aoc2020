open System.IO

let content =
    "input.txt"
    |> File.ReadAllText
    |> fun x -> x.Split "\n\n"
    |> Seq.map (fun x -> x.Replace("\n", ""))
    |> Seq.map Seq.distinct
    |> Seq.map Seq.length
    |> Seq.sum

[<EntryPoint>]
let main argv =
    content
    |> printfn "%A"
    0 // return an integer exit code