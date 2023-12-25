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

let getCalibrationFromDigits (s: string) =
    let values = s |> Seq.map asDigit |> Seq.where Option.isSome |> Seq.map Option.get
    let firstValue = values |> Seq.head
    let lastValue = values |> Seq.last
    let calibration = (firstValue * 10) + lastValue
    calibration

let part1 input =
    let models = input |> Seq.map toModel
    let calibrations = models |> Seq.map getCalibrationFromDigits
    let overallCalibration = calibrations |> Seq.reduce (+)
    overallCalibration

let part1Runner () =
    let overallCalibration = readInputFile "day1.txt" |> part1
    printf $"Day 1 Part 1 Answer: Overall calibration is {overallCalibration}\n"
    ()

type DigitPair = { Str: string; Value: int }

let digitPairs =
    [ { Str = "0"; Value = 0 }
      { Str = "zero"; Value = 0 }
      { Str = "1"; Value = 1 }
      { Str = "one"; Value = 1 }
      { Str = "2"; Value = 2 }
      { Str = "two"; Value = 2 }
      { Str = "3"; Value = 3 }
      { Str = "three"; Value = 3 }
      { Str = "4"; Value = 4 }
      { Str = "four"; Value = 4 }
      { Str = "5"; Value = 5 }
      { Str = "five"; Value = 5 }
      { Str = "6"; Value = 6 }
      { Str = "six"; Value = 6 }
      { Str = "7"; Value = 7 }
      { Str = "seven"; Value = 7 }
      { Str = "8"; Value = 8 }
      { Str = "eight"; Value = 8 }
      { Str = "9"; Value = 9 }
      { Str = "nine"; Value = 9 } ]

/// returns every substring of the input string that is formed by
/// successively removing the first character
let toTailSubstrings (s: string) =
    seq {
        for i in 0 .. s.Length do
            yield s[i..]
    }

let valueFromWordOrDigit (s: string) =
    let next = Seq.tryFind (fun dp -> s.StartsWith(dp.Str)) digitPairs
    next

let getCalibrationFromWords (s: string) =
    let values =
        s
        |> toTailSubstrings
        |> Seq.map valueFromWordOrDigit
        |> Seq.where Option.isSome
        |> Seq.map Option.get
        |> Seq.map _.Value

    let firstValue = values |> Seq.head
    let lastValue = values |> Seq.last
    let calibration = (firstValue * 10) + lastValue
    calibration

let part2 input =
    let models = input |> Seq.map toModel
    let calibrations = models |> Seq.map getCalibrationFromWords
    let overallCalibration = calibrations |> Seq.reduce (+)
    overallCalibration

let part2Runner () =
    let overallCalibration = readInputFile "day1.txt" |> part2
    printf $"Day 1 Part 2 Answer: Overall calibration is {overallCalibration}\n"
    ()
