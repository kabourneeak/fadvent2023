module FAdvent2023.Days.Day1

open FAdvent2023.Utils

let toModel s = s

let asDigit c =
    match c with
    | '0' -> Some(0)
    | '1' -> Some(1)
    | '2' -> Some(2)
    | '3' -> Some(3)
    | '4' -> Some(4)
    | '5' -> Some(5)
    | '6' -> Some(6)
    | '7' -> Some(7)
    | '8' -> Some(8)
    | '9' -> Some(9)
    | _ -> None

let getCalibration (s: string) =
    let values = s |> Seq.map asDigit |> Seq.filter Option.isSome |> Seq.map Option.get
    let firstValue = values |> Seq.head
    let lastValue = values |> Seq.last
    let calibration = (firstValue * 10) + lastValue
    calibration

let part1 input =
    let models = input |> Seq.map toModel
    let calibrations = models |> Seq.map getCalibration
    let overallCalibration = calibrations |> Seq.reduce (+)
    overallCalibration

let part1Runner () =
    let overallCalibration = readInputFile "day1.txt" |> part1
    printf $"Day 1 Part 1 Answer: Overall calibration is {overallCalibration}\n"
    ()
