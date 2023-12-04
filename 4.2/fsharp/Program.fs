open System.Linq
open System.IO

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let split (on: string) (s: string) =
    s.Split(on, System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.toList

let getMatchingNumbers (cardString: string) =
    let parseCardString cardString =
        cardString
        |> split ": "
        |> function
            | [_; numbers] -> numbers
            | _ -> failwith "Invalid card string"
        |> split " | "
        |> List.map (split " ")
        |> List.map (List.map int)
        |> function
            | [winners; numbers] -> (winners, numbers)
            | _ -> failwith "Invalid card string"

    let getMatchingNumbers (winners: 'a list, numbers: 'a list) =
        winners.Intersect numbers

    cardString
    |> parseCardString
    |> getMatchingNumbers
    |> Seq.length

// let data = 
//     [
//         "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
//         "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19";
//         "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1";
//         "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83";
//         "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36";
//         "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
//     ]

let data = File.ReadAllLines "../../4.input"

let numberOfCardsReducer (numberOfCards: int array) (index, cardString ) =
    let matchingNumbers = getMatchingNumbers cardString
    let numberOfCurrentCard = numberOfCards.[index]

    for i in 1 .. matchingNumbers do
        let offset = index + i
        numberOfCards.[offset] <- numberOfCards.[offset] + numberOfCurrentCard

    numberOfCards

let numberOfCards = Array.create data.Length 1

data
|> Array.mapi (fun i cardString -> (i, cardString))
|> Array.fold numberOfCardsReducer numberOfCards
|> Array.sum
|> printfn "%A"
