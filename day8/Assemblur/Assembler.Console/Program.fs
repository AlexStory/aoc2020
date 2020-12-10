open System.IO

let (|Acc|Jmp|Nop|) (str:string) =
    match str.Contains("jmp"), str.Contains("acc") with
    | true, _ -> Jmp
    | _, true -> Acc
    | _,_ -> Nop

let (|Add|Sub|) (str: string) =
    match str.Contains("+") with
    | true  -> Add (str.Split("+").[1] |> int)
    | false -> Sub (str.Split("-").[1] |> int)


let content =
    "input.txt"
    |> File.ReadAllLines


let rec parse (instructions: string []) acc idx history =
    let str = instructions.[idx]
    if Seq.contains idx history then
        acc
    else 
        match str with
        | Acc -> match str with 
                 | Add x -> parse instructions (acc + x) (idx + 1) (idx::history)
                 | Sub x -> parse instructions (acc - x) (idx + 1) (idx::history)
        | Jmp -> match str with
                 | Add x -> parse instructions acc (idx + x) (idx::history)
                 | Sub x -> parse instructions acc (idx - x) (idx::history)
        | Nop -> parse instructions acc (idx + 1) (idx::history)

[<EntryPoint>]
let main _argv =
    parse content 0 0 []
    |> printfn "%A" 
    0 // return an integer exit code