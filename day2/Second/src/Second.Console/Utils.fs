module Utils

let xor first second = 
    match first, second with
    | true, true -> false
    | true, false -> true
    | false, true -> true
    | false, false -> false