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

type Board = Seat [][]

module Board =
    let x spot = spot.X
    let y spot = spot.Y
    let seat spot = spot.Seat

    let maxX (board: Board) =
        board 
        |> Seq.head
        |> Seq.length
        |> fun x -> x - 1
    
    let maxY board =
        board
        |> Seq.length
        |> fun x -> x - 1

    let get (board: Board) x y =
        board.[y].[x]


    let up board spot =
        if y spot = 0 then
            true
        else
            match get board spot.X (spot.Y-1) with
            | Full -> false
            | _    -> true

    let left board spot = 
        if x spot = 0 then
            true
        else 
            match get board (spot.X-1) spot.Y with
            | Full -> false
            | _    -> true

    let down board spot =
        if y spot = maxY board then
            true
        else 
            match get board spot.X (spot.Y + 1) with
            | Full -> false
            | _    -> true

    let right board spot =
        if x spot = maxX board then
            true
        else 
            match get board (spot.X + 1) spot.Y with
            | Full -> false
            | _    -> true

    let upLeft board spot = 
        if x spot = 0 || y spot = 0 then
            true
        else 
            match get board (spot.X-1) (spot.Y-1) with
            | Full -> false
            | _    -> true

    let upRight board spot = 
        if x spot = maxX board || y spot = 0 then
            true
        else 
            match get board (spot.X+1) (spot.Y-1)  with
            | Full -> false
            | _    -> true

    let downLeft board spot =
        if x spot = 0 || y spot = maxY board then
            true
        else 
            match get board (spot.X-1) (spot.Y+1) with
            | Full -> false
            | _    -> true

    let downRight board spot =
        if x spot = maxX board || y spot = maxY board then
            true
        else 
            match get board (spot.X+1) (spot.Y+1) with
            | Full -> false
            | _    -> true

    let neighborCount board spot =
        [
            up board spot
            down board spot
            left board spot
            right board spot
            upLeft board spot
            upRight board spot
            downLeft board spot
            downRight board spot
        ] 
        |> List.filter not
        |> List.length

    let getNext board spot =
        match spot.Seat with
        | Floor                                   -> Floor
        | Empty when neighborCount board spot = 0 -> Full
        | Empty                                   -> Empty
        | Full when neighborCount board spot >= 4 -> Empty
        | Full                                    -> Full
        

    let step (board: Board): Board =
        [|
            for y, row in Array.indexed board do
                [|
                    for x, value in Array.indexed row do
                        getNext board { X = x; Y = y; Seat = value }
                |]
        |]

    let fullCount board =
        [
            for y in board do
                for x in y do
                    if x = Full then 1 else 0
        ]
        |> Seq.sum


let content: Board = 
    __SOURCE_DIRECTORY__ + "/input.txt"
    |> File.ReadAllLines
    |> Array.map(fun x -> x.ToCharArray() |> Array.map seat)


let rec loop board last iter=
    printfn "full: %A loop: %d" (Board.fullCount board) iter
    if board = last then
        board
    else 
        loop (Board.step board) board (iter+1)
    
[<EntryPoint>]
let main argv =
    loop content [||] 1
    |> Board.fullCount
    |> printfn "%A"
    0 // return an integer exit code