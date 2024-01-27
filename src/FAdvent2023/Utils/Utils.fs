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

[<TailCall>]
let rec adjacentPairs s =
    seq {
        if (Seq.isEmpty s) then
            ()
        else
            let l = s |> Seq.head
            let r = s |> Seq.skip 1 |> Seq.head

            yield (l, r)
            yield! adjacentPairs (Seq.skip 2 s)
    }
