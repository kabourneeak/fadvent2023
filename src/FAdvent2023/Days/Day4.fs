module FAdvent2023.Days.Day4

open FAdvent2023.Utils
open FAdvent2023.Extensions

type Card =
    { Id: int
      Winning: int Set
      Held: int Set }

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
          Winning = winning |> Set.ofSeq
          Held = held |> Set.ofSeq }

    input |> Seq.map toCard

let part1 input =
    let cards = input |> toCards

    let matchesPerCard =
        cards |> Seq.map (fun c -> Set.intersect c.Held c.Winning |> Set.count)

    let pointsPerCard =
        matchesPerCard
        |> Seq.where (fun m -> m > 0)
        // left-wise bit shift to do power of 2
        |> Seq.map (fun m -> 1 <<< (m - 1))

    pointsPerCard |> Seq.sum

let part1Runner () =
    let result = readInputFile "day4.txt" |> part1
    printf $"Day 4 Part 1 Answer: {result}\n"
    ()

let part2 input =

    let cards = input |> toCards

    let matchesPerCard =
        cards |> Seq.map (fun c -> Set.intersect c.Held c.Winning |> Set.count)

    // start with count = 1 for each card
    let countsPerCard = Array.create (Seq.length cards) 1

    matchesPerCard
    // for each card (by index)
    |> Seq.iteri (fun i m ->
        // simulate the generation of cards by updating the apparent count of each card
        for j in (i + 1) .. (i + m) do
            // based on how many of the current card have already been generated
            countsPerCard[j] <- countsPerCard[j] + countsPerCard[i])

    Array.sum countsPerCard

let part2Runner () =
    let result = readInputFile "day4.txt" |> part2
    printf $"Day 4 Part 2 Answer: {result}\n"
    ()
