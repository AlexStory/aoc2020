open System.IO


let getJolt (joltage, ones, threes) adapter =
    if joltage + 1 = adapter then
        (adapter, ones + 1, threes)
    elif joltage + 3 = adapter then
        (adapter, ones, threes + 1)
    else 
        (adapter, ones, threes)

let content = 
    __SOURCE_DIRECTORY__ + "/input.txt"
    |> File.ReadAllLines
    |> Seq.map int

let differences = 
    content
    |> Seq.sort
    |> Seq.fold getJolt (0, 0, 0)



[<EntryPoint>]
let main argv =
    differences
    |> fun (a, b, c) -> printfn "joltages: %d, ones: %d, threes: %d, answer: %d" a b c (b * (c + 1))
    0 // return an integer exit code