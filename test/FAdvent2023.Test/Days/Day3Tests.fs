module FAdvent2023.Test.Days.Day3Tests

open NUnit.Framework
open FAdvent2023.Days
open FAdvent2023.Days.Day3


[<Test>]
let ToGrid_ShouldCreateGridFromRow () =
    let input = [ "....." ]
    let result = Day3.toGrid input

    let expected =
        { Rows = 1
          Cols = 5
          Cells =
            Array2D.create
                1
                5
                { AdjacentToSymbol = false
                  Type = Empty } }

    Assert.That(result, Is.EqualTo(expected))

[<Test>]
let ToGrid_ShouldCreateGridFromCols () =
    let input = [ "."; "."; "."; "."; "." ]
    let result = Day3.toGrid input

    let expected =
        { Rows = 5
          Cols = 1
          Cells =
            Array2D.create
                5
                1
                { AdjacentToSymbol = false
                  Type = Empty } }

    Assert.That(result, Is.EqualTo(expected))

[<Test>]
let Part1_ShouldReturnExpectedSum () =
    let input =
        [ "467..114.."
          "...*......"
          "..35..633."
          "......#..."
          "617*......"
          ".....+.58."
          "..592....."
          "......755."
          "...$.*...."
          ".664.598.." ]

    let result = Day3.part1 input

    Assert.That(result, Is.EqualTo(4361))
