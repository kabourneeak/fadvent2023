module FAdvent2023.Test.Days.Day3Tests

open NUnit.Framework
open FAdvent2023.Days
open FAdvent2023.Days.Day3


[<Test>]
let ToGrid_ShouldCreateGridFromRow () =
    let input = [ "....." ]
    let result = Day3.toGrid input

    let expected = { Cells = Array2D.create 1 5 DefaultGridCell }

    Assert.That(result, Is.EqualTo(expected))

[<Test>]
let ToGrid_ShouldCreateGridFromCols () =
    let input = [ "."; "."; "."; "."; "." ]
    let result = Day3.toGrid input

    let expected = { Cells = Array2D.create 5 1 DefaultGridCell }

    Assert.That(result, Is.EqualTo(expected))

[<Test>]
[<TestCase("*1234", 1234)>]
[<TestCase(".1234", 0)>]
[<TestCase("1234*", 1234)>]
[<TestCase("1234.", 0)>]
let GetPartNumbers_ShouldGetNumber (line: string, expected: int) =
    let input = [ line ]

    let result = Day3.part1 input

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

[<Test>]
let Part2_ShouldReturnExpectedSum () =
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

    let result = Day3.part2 input

    Assert.That(result, Is.EqualTo(467835))
