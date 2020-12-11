// Program.fs
// Seatway
// aOc Day 10 - 2020
// Purposely did this one in a more enterprise like way

open System.IO

type Seat = Empty | Full | Floor

let seat = function
    | '.' -> Floor
    | 'L' -> Empty
    | '#' -> Full
    | _   -> failwith "Invalid seat"

type Spot = { 
    Seat: Seat
    X: int
    Y: int
}

type Board = Spot []

module Board =
    let x spot = spot.X
    let y spot = spot.Y
    let seat spot = spot.Seat

    let maxX (board: Board) =
        board 
        |> Seq.maxBy x
        |> x
    
    let maxY board =
        board
        |> Seq.maxBy y
        |> y

    let parse (seats: Seat [] []) =
        [
            for y, _ in Array.indexed seats do
                for x, _ in Array.indexed seats do
                    {
                        Seat = seats.[y].[x]
                        X = x
                        Y = y
                    }
        ]

    let get (board: Board) x y =
        Seq.find (fun s -> s.X = x && s.Y = y) board


    let up board spot =
        if y spot = 0 then
            true
        elif y spot = maxY board then
            true
        else
            match get board spot.X (spot.Y-1) |> seat with
            | Full -> false
            | _    -> true



let content = 
    __SOURCE_DIRECTORY__ + "/testinput.txt"
    |> File.ReadAllLines
    |> Array.map(fun x -> x.ToCharArray() |> Array.map seat)
    |> Board.parse

[<EntryPoint>]
let main argv =
    content
    |> printfn "%A"
    0 // return an integer exit code