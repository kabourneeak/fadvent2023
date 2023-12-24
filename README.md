# Advent of Code 2023 in F#

## Setting up your environment

Developing in this repository is best done with VS Code. You need to have the following things installed:

- The [Dotnet SDK 8](https://dotnet.microsoft.com/en-us/download)
- The [Ionide F# IDE](https://ionide.io) VS Code extension, `ionide.ionide-fsharp`
  - (included in the repository's VS Code workspace recommendations)
- The [Fantomas F# Source Code Formatter](https://fsprojects.github.io/fantomas/).
  - Fantomas is a dotnet tool. After installing the Dotnet SDK, you can install Fantomas with the following command:
  
    ```sh
    dotnet tool install -g fantomas
    ```

  - The VS Code workspace settings included in this repository will set Fantomas to format on save.

## Resources

- [FSharp Cheatsheet](https://fsprojects.github.io/fsharp-cheatsheet/)
- [Learn F#](https://dotnet.microsoft.com/en-us/learn/fsharp) from Microsoft
- [FSharp Documentation](https://fsharp.org/docs/) from the F# Software Foundation
- [FsUnit](https://github.com/fsprojects/FsUnit)
