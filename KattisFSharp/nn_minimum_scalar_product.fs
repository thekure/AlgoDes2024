module KattisFSharp.nn_minimum_scalar_product

open KattisFSharp.aaKattio

let scanner = Scanner()
let t = scanner.NextInt()

let readVector dim =
    let rec aux i acc =
        match i with
        | i when i = dim -> acc |> List.rev
        | i ->
            let value = scanner.NextInt()
            aux (i+1) (int64 value :: acc)
    aux 0 List.empty

let readInput =
    let rec aux i acc =
        match i with
        | i when i = t -> acc |> List.rev
        | i ->
            let dimensions = scanner.NextInt()
            let v1 = readVector dimensions
            let v2 = readVector dimensions
            aux (i+1) ((v1,v2)::acc)
    aux 0 List.empty
    
let rec calculateScalarProduct v1 v2 acc =
    match v1, v2 with
    | [], [] -> int acc
    | x1::xs1, x2::xs2 -> 
        let product = x1 * x2
        calculateScalarProduct xs1 xs2 (acc + product)
    | _ -> failwith "Vectors must be of the same length."

let handleCase (v1,v2) =
    let result1 =
        let smallestV1 = List.sort v1
        let largestV2 = List.sortDescending v2
        calculateScalarProduct smallestV1 largestV2 0L
    let result2 =
        let largestV1 = List.sortDescending v1
        let smallestV2 = List.sort v2
        calculateScalarProduct largestV1 smallestV2 0L
    if result1 <= result2 then result1 else result2

let handleAllCases cases =
    let rec aux i cases acc =
        match i, cases with
        | i, _ when i = t -> acc |> List.rev
        | i, case :: tail ->
            aux (i+1) tail ((handleCase case) :: acc)
        | _,_ ->
            printf $"handleAllCases: This really shouldn't be happening\n"
            acc
    aux 0 cases List.empty

let printResults (lst: int list) =
    let rec aux case lst =
        match case, lst with
        | case, _ when case = t -> ()
        | case, result::tail ->
            printf $"Case #{(case + 1)}: {result}\n"
            aux (case + 1) tail
        | _,_ ->
            printf $"printResults: This really shouldn't be happening\n"
    aux 0 lst
let loop =
    // cases is a list of (v1, v2) pairs
    let cases = readInput
    let results = handleAllCases cases
    printResults results
    
    ()
    
let runMinimum = loop
