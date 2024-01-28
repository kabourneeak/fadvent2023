module FAdvent2023.Test.Days.Day6Tests

open NUnit.Framework
open FAdvent2023.Days

let exampleInput = [ "Time:      7  15   30"; "Distance:  9  40  200" ]

[<Test>]
let Part1_ShouldHaveExpectedResult () =
    let result = Day6.part1 exampleInput

    Assert.That(result, Is.EqualTo(288))

[<Test>]
let Part2_ShouldHaveExpectedResult () =
    let result = Day6.part2 exampleInput

    Assert.That(result, Is.EqualTo("n/a"))
