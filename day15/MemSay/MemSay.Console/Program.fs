open System.IO
open FSharpPlus

let content = 
            __SOURCE_DIRECTORY__ + "/input.txt"
            |> File.ReadAllText
            |> String.split [","]
            |> map int 

let rec play (game: int[]) (goal: int) (value: int) (turn: int) =
    let newVal =
        match game.[value] with
        | -1 -> 0
        | n  -> turn - n

    if turn = goal then
        value
    else 
        game.[value] <- turn
        play game goal newVal (turn + 1)


let run size =
    let second = Array.init size (konst -1)
    Seq.iteri (fun i v -> second.[v] <- i + 1 ) content

    play second size 0 ((length content) + 1)
    |> printfn "part 2: %A" 
    
[<EntryPoint>]
let main _argv =
    run 30_000_000
    0