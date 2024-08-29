module KattisFSharp.zzbeeneverywhere

open KattisFSharp.aaKattio

// First line: int <= 50 = number of test cases

// Each test case:
// int n (number of work trips so far)
// n lines with city names

let scanner = Scanner()
let testCaseCount = scanner.NextInt()

let countUniqueStrings lst =
    lst |> Set.ofList |> Set.count

let scanCity count =
    let rec aux acc i = 
        match i with
        | 0 -> acc
        | _ ->
            let city = scanner.Next()
            aux (city::acc) (i-1)
    aux [] count
    
let handleTestCase () =
    let cityCount = scanner.NextInt()
    let cities = scanCity cityCount
    printf $"%i{countUniqueStrings cities}\n"



let runBeenEverywhere=
    let rec aux loops =
        match loops with
        | 0 -> ()
        | _ ->
            handleTestCase()
            aux (loops-1)
    aux testCaseCount

