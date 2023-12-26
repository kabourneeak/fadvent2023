module FAdvent2023.Days.Day3

open FAdvent2023.Utils

type CellType =
    | Digit of value: int
    | Symbol
    | Empty

type GridCell =
    { Type: CellType
      AdjacentToSymbol: bool }

type Grid =
    { Rows: int
      Cols: int
      Cells: GridCell array2d }

let unwrapDigit c =
    match c with
    | Digit d -> d
    | _ -> raise (System.ArgumentOutOfRangeException(nameof c))

let toGrid (input: list<string>) =
    let rows = input.Length
    let cols = input[0].Length

    let emptyGrid =
        Array2D.create
            rows
            cols
            { Type = Empty
              AdjacentToSymbol = false }

    let populatedGrid =
        emptyGrid
        |> Array2D.mapi (fun row col gridcell ->
            let cellType =
                match input[row][col] with
                | '0' -> Digit(0)
                | '1' -> Digit(1)
                | '2' -> Digit(2)
                | '3' -> Digit(3)
                | '4' -> Digit(4)
                | '5' -> Digit(5)
                | '6' -> Digit(6)
                | '7' -> Digit(7)
                | '8' -> Digit(8)
                | '9' -> Digit(9)
                | '.' -> Empty
                | _ -> Symbol

            { gridcell with Type = cellType })

    let isAdjacentToSymbol array row col =
        let adjacencies =
            [ (-1, -1)
              (-1, 0)
              (-1, +1)
              (0, -1)
              (0, 0)
              (0, +1)
              (+1, -1)
              (+1, 0)
              (+1, +1) ]

        let isInBounds (row, col) =
            row >= 0 && row < rows && col >= 0 && col < cols

        let neighbours =
            adjacencies
            |> Seq.map (fun (adjRow, adjCol) -> (row + adjRow, col + adjCol))
            |> Seq.where isInBounds
            |> Seq.map (fun (row, col) -> Array2D.get array row col)

        let atLeastOneNearbySymbol = neighbours |> Seq.exists (fun n -> n.Type = Symbol)

        atLeastOneNearbySymbol

    let adjecencyGrid =
        populatedGrid
        |> Array2D.mapi (fun row col value ->
            { value with
                AdjacentToSymbol = isAdjacentToSymbol populatedGrid row col })

    { Rows = rows
      Cols = cols
      Cells = adjecencyGrid }

let getPartNumbers (grid: Grid) =
    let toPartNum l =
        let hasAdjacentSymbol = l |> Seq.exists _.AdjacentToSymbol
        let digits = l |> Seq.map _.Type |> Seq.map unwrapDigit
        let value = digits |> Seq.fold (fun t o -> t * 10 + o) 0

        match (value, hasAdjacentSymbol) with
        | (0, _) -> None
        | (_, false) -> None
        | _ -> Some(value)

    let mutable accum = List.empty
    let mutable partNums = List.empty

    for r in 0 .. grid.Rows - 1 do
        for c in 0 .. grid.Cols - 1 do
            let cell = Array2D.get grid.Cells r c

            match cell.Type with
            | Digit _ -> accum <- List.append accum [ cell ]
            | _ ->
                partNums <- List.append partNums [ toPartNum accum ]
                accum <- []

        partNums <- List.append partNums [ toPartNum accum ]
        accum <- []

    partNums |> Seq.where Option.isSome |> Seq.map Option.get

let part1 input =
    let grid = toGrid input
    let partNumbers = getPartNumbers grid

    partNumbers |> Seq.sum

let part1Runner () =
    let sumOfPartNumbers = readInputFile "day3.txt" |> part1
    printf $"Day 3 Part 1 Answer: Sum of part numbers is {sumOfPartNumbers}\n"
    ()
