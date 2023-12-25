module FAdvent2023.Utils

open System.IO

let private slnName = "FAdvent2023.sln"

let private containsSln path =
    File.Exists(Path.Combine(path, slnName))

[<TailCall>]
let rec private getProjectRoot startDir =
    match containsSln startDir with
    | true -> startDir
    | false -> getProjectRoot (Directory.GetParent(startDir).ToString())

let readInputFile fileName =
    let projectRoot = getProjectRoot (Directory.GetCurrentDirectory())
    let inputPath = Path.Combine(projectRoot, "input", fileName)
    let lines = File.ReadAllLines(inputPath)
    Seq.toList lines
