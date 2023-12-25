namespace FAdvent2023.Extensions

open System.Runtime.CompilerServices

[<Extension>]
type StringExtensions =
    [<Extension>]
    static member inline Break (xs: string) (breakOn: string) =
        match xs.Split breakOn with
        | [| left; right |] -> (left, right)
        | _ -> raise (System.ArgumentOutOfRangeException xs)
