module FAdvent2023.Days.Day3

open FAdvent2023.Utils

type CellType =
    | Digit of value: int
    | Symbol
    | Empty

type GridCell =
    { Type: CellType
      Value: Option<int>
      ValueId: Option<int> }

let DefaultGridCell =
    { Type = Empty
      Value = None
      ValueId = None }

type Grid = { Cells: GridCell array2d }

let toIndexedSequence (array: 'a array2d) =
    seq {
        for row in 0 .. (Array2D.length1 array) - 1 do
            for col in 0 .. (Array2D.length2 array) - 1 do
                yield (row, col, Array2D.get array row col)
    }

let unwrapDigit c =
    match c with
    | Digit d -> d
    | _ -> invalidArg (nameof c) "Cannot unwrap other cell types"

let isDigit c =
    match c with
    | Digit _ -> true
    | _ -> false

let isSymbol c =
    match c with
    | Symbol -> true
    | _ -> false

let toGrid (input: list<string>) =
    let rows = input.Length
    let cols = input[0].Length

    let cells = Array2D.create rows cols DefaultGridCell

    // populate cells with token types
    cells
    |> Array2D.iteri (fun row col gridcell ->
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

        Array2D.set cells row col { gridcell with Type = cellType })

    let mutable nextValueId = 0

    let extractValue row col =
        // identify cells containing sequential digits
        let rowSlice = cells[row, col..]
        let digitCells = rowSlice |> Seq.takeWhile (fun c -> isDigit c.Type)

        // combine digits into scalar value
        let digits = digitCells |> Seq.map _.Type |> Seq.map unwrapDigit
        let value = digits |> Seq.fold (fun t o -> t * 10 + o) 0

        // assign id
        let valueId = nextValueId
        nextValueId <- nextValueId + 1

        // write back to array
        digitCells
        |> Seq.iteri (fun i g ->
            Array2D.set
                cells
                row
                (col + i)
                { g with
                    Value = Some(value)
                    ValueId = Some(valueId) })

        ()

    // populate cells with values created from consequtive digits
    cells
    |> Array2D.iteri (fun row col gridcell ->
        match (gridcell.Type, gridcell.ValueId) with
        | (Digit _, None) -> extractValue row col
        | _ -> ())

    { Cells = cells }

let neighbours (array: GridCell array2d) row col =
    let rows = Array2D.length1 array
    let cols = Array2D.length2 array

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

    adjacencies
    |> Seq.map (fun (adjRow, adjCol) -> (row + adjRow, col + adjCol))
    |> Seq.where isInBounds
    |> Seq.map (fun (row, col) -> Array2D.get array row col)

let getPartNumbers (grid: Grid) =
    grid.Cells
    |> toIndexedSequence
    // find symbols first, as there are fewer of them
    |> Seq.where (fun (_, _, g) -> isSymbol g.Type)
    // collect all the neighbours of symbols
    |> Seq.collect (fun (r, c, g) -> neighbours grid.Cells r c)
    // reduce to distinct values by value id
    |> Seq.where (fun g -> Option.isSome g.Value)
    |> Seq.distinctBy (fun g -> g.ValueId)
    // get the values
    |> Seq.map _.Value
    |> Seq.map Option.get

let part1 input =
    let grid = toGrid input
    let partNumbers = getPartNumbers grid

    partNumbers |> Seq.sum

let part1Runner () =
    let sumOfPartNumbers = readInputFile "day3.txt" |> part1
    printf $"Day 3 Part 1 Answer: Sum of part numbers is {sumOfPartNumbers}\n"
    ()
