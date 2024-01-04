module FAdvent2023.Days.Day5

open FAdvent2023.Utils
open FAdvent2023.Extensions

type Range =
    { Start: int64
      End: int64
      Offset: int64 }

type RangeMap =
    { Source: string
      Dest: string
      Ranges: Range seq }

let DefaultRangeMap =
    { Source = ""
      Dest = ""
      Ranges = Seq.empty }

type Almanac =
    { Seeds: int64 seq
      MapsByDest: Map<string, RangeMap> }

let DefaultAlmanac =
    { Seeds = Seq.empty
      MapsByDest = Map.empty }

type ReadState =
    | SeedRow
    | MapHeader
    | MapRow

let toAlmanac (input: string seq) =

    let mutable readState = SeedRow
    let mutable almanac = DefaultAlmanac
    let mutable rangeMap = DefaultRangeMap

    let processLine (line: string) =
        match (readState, line) with
        | (SeedRow, _) ->
            let seedSegment = line.Replace("seeds: ", "")
            let seeds = seedSegment.Split(' ') |> Seq.map int64
            almanac <- { almanac with Seeds = seeds }
            readState <- MapHeader

        | (MapHeader, "") -> ()
        | (MapHeader, _) ->
            let nameSegment = line.Replace(" map:", "")
            let nameParts = nameSegment.Split('-')
            let source = nameParts[0]
            let dest = nameParts[2]

            rangeMap <-
                { DefaultRangeMap with
                    Source = source
                    Dest = dest }

            readState <- MapRow

        | (MapRow, "") ->
            almanac <-
                { almanac with
                    MapsByDest = almanac.MapsByDest.Add(rangeMap.Dest, rangeMap) }

            rangeMap <- DefaultRangeMap
            readState <- MapHeader

        | (MapRow, _) ->
            let rangeArgs = line.Split(' ') |> Seq.map int64 |> Seq.toList

            let range =
                { Start = rangeArgs[1]
                  End = rangeArgs[1] + rangeArgs[2] - 1L
                  Offset = rangeArgs[0] - rangeArgs[1] }

            rangeMap <-
                { rangeMap with
                    Ranges = (Seq.append rangeMap.Ranges (Seq.singleton range)) }

    // append extra line to end of input to ensure final range is processed
    Seq.append input (Seq.singleton "")
    // then process the sequence
    |> Seq.iter processLine

    almanac

let applyRangeMap map value =
    let range = map.Ranges |> Seq.tryFind (fun r -> r.Start <= value && value <= r.End)

    match range with
    | Some(r) -> value + r.Offset
    | None -> value

let applyRangeMapOrder almanac mapOrder seed =
    mapOrder
    // get range maps in order
    |> Seq.map (fun o -> almanac.MapsByDest[o])
    // successively apply to seed value in order
    |> Seq.fold (fun s m -> applyRangeMap m s) seed

let part1 input =
    let almanac = input |> toAlmanac

    let mapOrder =
        [ "soil"
          "fertilizer"
          "water"
          "light"
          "temperature"
          "humidity"
          "location" ]

    let seedLocations =
        almanac.Seeds |> Seq.map (fun s -> applyRangeMapOrder almanac mapOrder s)

    seedLocations |> Seq.min

let part1Runner () =
    let result = readInputFile "day5.txt" |> part1
    printf $"Day 5 Part 1 Answer: {result}\n"
    ()

let part2 input = "n/a"

let part2Runner () =
    let result = readInputFile "day5.txt" |> part2
    printf $"Day 5 Part 2 Answer: {result}\n"
    ()
