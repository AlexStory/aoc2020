namespace First

module Console =
    open System.IO

    type Spot = Tree | Plain
    let getSpot = function
    | '#' -> Tree
    | '.' -> Plain
    | _ -> failwith "Invalid Argument"

    let content = File.ReadAllLines "assets/input.txt"
    let x, y = 0, 0
    let xStep = 3
    let yStep = 1
    let height = Array.length content
    let trackWidth = content |> Array.head |> String.length
    let rebalanceX x = x % trackWidth

    let rec count x y trees =
        let xTarget = rebalanceX (x + xStep)
        let yTarget = y + yStep

        if yTarget > height - 1 then 
            trees
        else
            match getSpot content.[yTarget].[xTarget] with
            | Tree -> count xTarget yTarget (trees + 1)
            | Plain -> count xTarget yTarget trees

    [<EntryPoint>]
    let main argv =
        count 0 0 0
        |> printfn "%A"
        0 // return an integer exit code