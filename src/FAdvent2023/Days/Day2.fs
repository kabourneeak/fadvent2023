module FAdvent2023.Days.Day2

open FAdvent2023.Utils
open FAdvent2023.Extensions

type Draw = { Red: int; Green: int; Blue: int }

type Game = { Id: int; Draws: List<Draw> }

/// input example: 1 blue
let private reduceColor (draw: Draw) (colourCount: string) =
    match (StringExtensions.Break colourCount " ") with
    | (c, "red") -> { draw with Red = (c |> int) }
    | (c, "green") -> { draw with Green = (c |> int) }
    | (c, "blue") -> { draw with Blue = (c |> int) }
    | _ -> raise (System.NotImplementedException())

/// input example: 3 green, 4 blue, 1 red
let private toDrawModel (drawSegment: string) =
    let colourCounts = drawSegment.Split ", "
    colourCounts |> Seq.fold reduceColor { Red = 0; Green = 0; Blue = 0 }

/// parse game input into a model
/// input example /// Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
let toGameModel (s: string) =
    let idSegment, gameSegment = StringExtensions.Break s ": "
    let id = idSegment[5..] |> int
    let drawSegments = gameSegment.Split("; ")
    let draws = drawSegments |> Seq.map toDrawModel |> Seq.toList
    { Id = id; Draws = draws }

let drawIsPossible bag draw =
    draw.Red <= bag.Red && draw.Blue <= bag.Blue && draw.Green <= bag.Green

let gameIsPossible bag game =
    game.Draws |> Seq.forall (drawIsPossible bag)

let part1 input =
    let games = input |> Seq.map toGameModel
    let bag = { Red = 12; Green = 13; Blue = 14 }

    let sumOfPossibleGames =
        games |> Seq.where (gameIsPossible bag) |> Seq.map _.Id |> Seq.sum

    sumOfPossibleGames

let part1Runner () =
    let sumOfGames = readInputFile "day2.txt" |> part1
    printf $"Day 2 Part 1 Answer: Sum of possible game IDs is {sumOfGames}\n"
    ()
