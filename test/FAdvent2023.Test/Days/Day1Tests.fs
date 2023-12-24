module FAdvent2023.Test.Days.Day1Tests

open NUnit.Framework
open FAdvent2023.Days

[<SetUp>]
let Setup () = ()

[<Test>]
let Test1 () =
    let result = Day1.factorialWithAcc 5u 1u
    Assert.That(result, Is.EqualTo(120))
