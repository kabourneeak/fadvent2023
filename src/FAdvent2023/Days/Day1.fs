module FAdvent2023.Days.Day1

[<TailCall>]
let rec factorialWithAcc n accumulator =
    match n with
    | 0u
    | 1u -> accumulator
    | _ -> factorialWithAcc (n - 1u) (n * accumulator)
