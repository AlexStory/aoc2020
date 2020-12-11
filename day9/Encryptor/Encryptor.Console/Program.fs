open System.IO

let getCombinations list =
    let mutable arr = []
    for x in list do
        for y in list do
            if x <> y then
                let res = x + y
                arr <- res :: arr
    arr


let content = 
    "input.txt"
    |> File.ReadAllLines
    |> Seq.map bigint.Parse


let subset = content |> Seq.skip 25

let possibilities i = 
    content 
        |> Seq.skip (i) 
        |> (Seq.take 25)
        |> getCombinations 

let isValid i v = 
    let possibles = possibilities i
    Seq.contains v possibles

subset
    |> Seq.indexed
    |> Seq.filter (fun (i, x) -> not (isValid i x))
    |> Seq.iter (printfn "%A")




[<EntryPoint>]
let main argv =
    // "alex"
    // |> printfn "%A"
    0 // return an integer exit code