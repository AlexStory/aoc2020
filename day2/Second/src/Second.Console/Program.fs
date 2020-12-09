open System.IO
open Second

let content = 
    "assets/input.txt"
    |> File.ReadAllLines
    |> Array.map(Password.Init)
    |> Array.filter(fun x -> x.Valid)


[<EntryPoint>]
let main argv =
    content
    |> Array.length
    |> printfn "%A" 
    0