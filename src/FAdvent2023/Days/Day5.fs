module FAdvent2023.Days.Day5

open FAdvent2023.Utils

type Interval = { Start: int64; End: int64 }

type OffsetInterval = { Interval: Interval; Offset: int64 }

type RangeMap =
    { Source: string
      Dest: string
      Ranges: OffsetInterval seq }

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
                { Interval =
                    { Start = rangeArgs[1]
                      End = rangeArgs[1] + rangeArgs[2] - 1L }
                  Offset = rangeArgs[0] - rangeArgs[1] }

            rangeMap <-
                { rangeMap with
                    Ranges = (Seq.append rangeMap.Ranges (Seq.singleton range)) }

    // append extra line to end of input to ensure final range is processed
    Seq.append input (Seq.singleton "")
    // then process the sequence
    |> Seq.iter processLine

    // return our completed almanac
    almanac

let part1 input =
    let almanac = input |> toAlmanac

    let rangeMapOrder =
        [ "soil"
          "fertilizer"
          "water"
          "light"
          "temperature"
          "humidity"
          "location" ]

    let rangeMaps = rangeMapOrder |> Seq.map (fun o -> almanac.MapsByDest[o])

    let applyRangeMap map value =
        let range =
            map.Ranges
            |> Seq.tryFind (fun r -> r.Interval.Start <= value && value <= r.Interval.End)

        match range with
        | Some(r) -> value + r.Offset
        | None -> value

    let applyRangeMaps maps seed =
        maps |> Seq.fold (fun s m -> applyRangeMap m s) seed

    let seedLocations = almanac.Seeds |> Seq.map (fun s -> applyRangeMaps rangeMaps s)

    seedLocations |> Seq.reduce (fun a c -> min a c)

let part1Runner () =
    let result = readInputFile "day5.txt" |> part1
    printf $"Day 5 Part 1 Answer: {result}\n"
    ()

let intersection (i1: Interval) (i2: Interval) =
    if (i1.End < i2.Start || i1.Start > i2.End) then
        None
    else
        Some(
            { Start = max i1.Start i2.Start
              End = min i1.End i2.End }
        )

let part2 input =
    let almanac = input |> toAlmanac

    let rangeMapOrder =
        [ "soil"
          "fertilizer"
          "water"
          "light"
          "temperature"
          "humidity"
          "location" ]

    // get range maps in order
    let rangeMaps = rangeMapOrder |> Seq.map (fun o -> almanac.MapsByDest[o])

    let seedIntervals =
        almanac.Seeds
        |> adjacentPairs
        |> Seq.map (fun (s, e) -> { Start = s; End = s + e - 1L })

    let applyToInterval (interval: Interval) (mapRanges: OffsetInterval seq) =
        mapRanges
        |> Seq.map (fun m ->
            match (intersection m.Interval interval) with
            | None -> None
            | Some i ->
                Some(
                    { Start = i.Start + m.Offset
                      End = i.End + m.Offset }
                ))
        |> Seq.where Option.isSome
        |> Seq.map Option.get

    let applyToIntervals (intervals: Interval seq) (mapRanges: OffsetInterval seq) =
        intervals |> Seq.collect (fun i -> applyToInterval i mapRanges)

    // create map ranges with offset 0 between the given ranges so that we have
    // a full set of map ranges from 0 to 2^64
    let fillMapRange (mapRanges: OffsetInterval seq) =
        let sortedMapRanges = mapRanges |> Seq.sortBy _.Interval.Start |> Seq.toList

        seq {
            let mutable lastEnd = -1L

            for i in 0 .. (sortedMapRanges.Length - 1) do
                let mapRange = sortedMapRanges[i]

                if (lastEnd < (mapRange.Interval.Start - 1L)) then
                    yield
                        { Interval =
                            { Start = lastEnd + 1L
                              End = mapRange.Interval.Start - 1L }
                          Offset = 0 }

                yield mapRange
                lastEnd <- mapRange.Interval.End

            if (lastEnd < System.Int64.MaxValue) then
                yield
                    { Interval =
                        { Start = lastEnd + 1L
                          End = System.Int64.MaxValue }
                      Offset = 0 }
        }

    // transform initial seed intervals through successive maps to arrive at locations
    let locationIntervals =
        rangeMaps
        |> Seq.map _.Ranges
        |> Seq.map fillMapRange
        |> Seq.fold applyToIntervals seedIntervals

    // return the smallest value of all the intervals
    locationIntervals |> Seq.map _.Start |> Seq.min

let part2Runner () =
    let result = readInputFile "day5.txt" |> part2
    printf $"Day 5 Part 2 Answer: {result}\n"
    ()
