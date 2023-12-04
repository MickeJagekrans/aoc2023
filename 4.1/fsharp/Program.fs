open System.IO
open FSharp.Core
open System
open System.Linq

// Example line:
// Card   1: 13  5 40 15 21 61 74 55 32 56 | 21 57 74 56  7 84 37 47 75 66 68  8 55 22 53 61 40 13 15 41 32 46 95 65

let lines =
    File.ReadAllLines("../../4.input")
    |> Array.toList

let split (char: char) (str: string) =
    str.Split(char, StringSplitOptions.RemoveEmptyEntries)
    |> Array.toList

let getCardParts (line: string) =
    line
    |> split ':'
    |> function
        | [_; numbers] -> numbers
        | _ -> failwith "Invalid input"
    |> split '|'
    |> Seq.map (split ' ')
    |> Seq.map (Seq.map int)
    |> Seq.toList
    |> function
        | [winners; numbers] -> winners, numbers
        | _ -> failwith "Invalid input"

let getCardScore (winners: int seq, numbers: int seq) = 
    match (winners.Intersect numbers) |> Seq.toList with
    | [] -> 0
    | l -> pown 2 (List.length l - 1)

lines
|> Seq.map (getCardParts >> getCardScore)
|> Seq.sum
|> printfn "%d"
