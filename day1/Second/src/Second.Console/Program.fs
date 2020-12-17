open System.IO
let contents = 
    __SOURCE_DIRECTORY__ + "/input.txt"
    |> File.ReadAllLines 
    |> Array.map int

let mutable a, b, c = 0, 0, 0

for x in contents do
    for y in contents do
        if Seq.contains (2020 - x - y) contents then
            printfn "%d" (x * y * (2020 - x - y))
                

[<EntryPoint>]
let main argv =
    0 // return an integer exit code