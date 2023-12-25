module FAdvent2023.Test.Days.Day1Tests

open NUnit.Framework
open FAdvent2023.Days

[<Test>]
[<TestCase("1abc2", 12)>]
[<TestCase("pqr3stu8vwx", 38)>]
[<TestCase("a1b2c3d4e5f", 15)>]
[<TestCase("treb7uchet", 77)>]
let Part1_ShouldReturnExpectedValueForEachSampleLine input expected =
    let lines = [ input ]
    let result = Day1.part1 lines
    Assert.That(result, Is.EqualTo(expected))

[<Test>]
let Part1_ShouldReturnExpectedValueForOverallSample () =
    let lines = [ "1abc2"; "pqr3stu8vwx"; "a1b2c3d4e5f"; "treb7uchet" ]
    let result = Day1.part1 lines
    Assert.That(result, Is.EqualTo(142))
