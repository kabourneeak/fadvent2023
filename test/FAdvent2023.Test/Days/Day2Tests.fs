module FAdvent2023.Test.Days.Day2Tests

open NUnit.Framework
open FAdvent2023.Days
open FAdvent2023.Days.Day2

[<Test>]
let ToGameModel_ShouldReturnExpectedModel () =
    let input = "Game 12: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
    let result = Day2.toGameModel input

    let expectedResult =
        { Id = 12
          Draws =
            [ { Red = 4; Green = 0; Blue = 3 }
              { Red = 1; Green = 2; Blue = 6 }
              { Red = 0; Green = 2; Blue = 0 } ] }

    Assert.That(result, Is.EqualTo(expectedResult))

[<Test>]
let Part1_ShouldReturnExpectedSum () =
    let input =
        [ "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
          "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"
          "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"
          "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"
          "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green" ]

    let result = Day2.part1 input

    Assert.That(result, Is.EqualTo(8))
