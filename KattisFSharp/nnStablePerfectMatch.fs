module KattisFSharp.nnStablePerfectMatch

open System.Collections.Generic
open KattisFSharp.aaKattio
open KattisFSharp.aaKattioWithLineReader

// n is 2-200 inclusive, even
// m = connecting edges

let scanner = Scanner()
let n = scanner.NextInt()
let m = scanner.NextInt()
    
let register () =
    printf "\n"
    let subject = scanner.Next()
    let prefsArr = scanner.NextLine()               // Scan pref list
    let prefsLst = Seq.toList prefsArr |> List.rev  // Convert to list
        
    let rec fillMap length (lst: string list) = // Remember priorities in map
        let rec aux acc index =
            match index with
            | i when i = length -> acc
            | i -> aux (Map.add lst[i] (i+1) acc) (i+1)
        aux Map.empty 0
        
    let prefsMap = fillMap prefsArr.Length prefsLst // Fill map
    
    (subject, prefsLst, prefsMap)

let rec readData (names, lists, maps) count =
        match count with
        | 0 -> ((names |> List.rev), lists, maps)
        | _ ->
            let name, prefStack, prefMap = register()
            let updatedStacks = Map.add name prefStack lists
            let updatedMaps = Map.add name prefMap maps
            let updatedNames = name::names
            readData (updatedNames, updatedStacks, updatedMaps) (count-1)



let loop =
    let names, stacks, maps = (readData (List.empty, Map.empty, Map.empty) n)
    let matchMap = Map.empty
    let proposers = List.skip (n/2) (List.rev names) |> List.rev
    let rejecters = List.skip (n/2) names
    
   
    () // All data is now available with names being keys in either map.
    
    
    
    

let runStablePerfectMatch = loop