module FAdvent2023.Days.Day4

open FAdvent2023.Utils
open FAdvent2023.Extensions

type Card =
    { Id: int
      Winning: int seq
      Held: int seq }

let toCards input =
    let toCard line =
        let idSegment, gameSegment = StringExtensions.Break line ": "
        let winningSegment, heldSegment = StringExtensions.Break gameSegment " | "

        let id = (idSegment[5..]).Trim() |> int

        let winning =
            winningSegment.Split(
                " ",
                System.StringSplitOptions.TrimEntries
                ||| System.StringSplitOptions.RemoveEmptyEntries
            )
            |> Seq.map int

        let held =
            heldSegment.Split(
                " ",
                System.StringSplitOptions.TrimEntries
                ||| System.StringSplitOptions.RemoveEmptyEntries
            )
            |> Seq.map int

        { Id = id
          Winning = winning
          Held = held }

    input |> Seq.map toCard

let part1 input =
    let cards = input |> toCards

    let matches =
        cards
        |> Seq.map (fun c -> c.Held |> Seq.where (fun h -> Seq.contains h c.Winning) |> Seq.length)

    let points =
        matches |> Seq.where (fun m -> m > 0) |> Seq.map (fun m -> 1 <<< (m - 1))

    points |> Seq.sum

let part1Runner () =
    let result = readInputFile "day4.txt" |> part1
    printf $"Day 4 Part 1 Answer: {result}\n"
    ()

let part2 input = "n/a"

let part2Runner () =
    let result = readInputFile "day4.txt" |> part2
    printf $"Day 4 Part 2 Answer: {result}\n"
    ()
