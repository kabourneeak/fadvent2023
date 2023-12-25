# Advent of Code 2023 in F#

[Advent of Code 2023](https://adventofcode.com/2023)

## Setting up your environment

Developing in this repository is best done with VS Code.
You need to have the following things installed:

- The [Dotnet SDK 8](https://dotnet.microsoft.com/en-us/download)
- The [Ionide F# IDE](https://ionide.io) VS Code extension, `ionide.ionide-fsharp`
  - (included in the repository's VS Code workspace recommendations)
- The [Fantomas F# Source Code Formatter](https://fsprojects.github.io/fantomas/).
  - Fantomas is a dotnet tool. After installing the Dotnet SDK, you can install Fantomas with the following command:
  
    ```sh
    $ dotnet tool install -g fantomas
    ```

  - The VS Code workspace settings included in this repository will set Fantomas to format on save.

## Running the program

From the root of the repository,
the project can be run by:

```sh
$ dotnet run --project src/FAdvent2023
```

You need to supply the Advent of Code input files,
which should be saved in the `input/` folder of the repository.

The files should be named in the style of `day1.txt`.
If part 2 has a separate input file,
it should be named in the style of `day1part2.txt`.

## Resources

- [FSharp Cheatsheet](https://fsprojects.github.io/fsharp-cheatsheet/)
- [FSharp Documentation](https://fsharp.org/docs/) from the F# Software Foundation
  - [FSharp Core Library Documentation](https://fsharp.github.io/fsharp-core-docs/)
- [Learn F#](https://dotnet.microsoft.com/en-us/learn/fsharp) from Microsoft
- [FsUnit](https://github.com/fsprojects/FsUnit)
