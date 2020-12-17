open System.IO

let getCombinations (list: seq<int64>) =
    [|
        for x in list do
            for y in list do
                if x <> y then
                    x + y
    |]


let content = 
    __SOURCE_DIRECTORY__ + "/input.txt"
    |> File.ReadAllLines
    |> Array.map int64


let subset = content |> Seq.skip 25

let possibilities i = 
    content 
        |> Array.skip (i) 
        |> (Array.take 25)
        |> getCombinations 

let isValid i v = 
    let possibles = possibilities i
    Array.contains v possibles


let rec findSum n list=
    if Array.isEmpty list then 
        (0, 0)
    else 
        let result  = 
            list
            |> Seq.scan (fun acc (i , num) -> (i, num + snd acc)) (0, 0L)
        match Seq.contains n (Seq.tail result |> Seq.map snd) with
        | true -> 
            let start = Seq.head list |> fst
            let stop = Seq.find (fun x -> snd x = n) result |> fst
            (start, stop)
        | false -> findSum n (Array.tail list)

let summate n list = 
    findSum n (Array.indexed list)

// subset
//     |> Seq.indexed
//     |> Seq.filter (fun (i, x) -> not (isValid i x))
//     |> Seq.iter (printfn "%A")

[<EntryPoint>]
let main argv =
    let n =  217430975L
    summate n content
    |> fun (start, stop) -> Seq.toArray(content).[start..stop]
    |> fun lst -> (Seq.min(lst)) + (Seq.max(lst))
    |> printfn "%A"
    0 // return an integer exit code