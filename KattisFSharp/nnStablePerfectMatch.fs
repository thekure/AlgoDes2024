module KattisFSharp.nnStablePerfectMatch

open System.Collections.Generic
open KattisFSharp.aaKattio
open KattisFSharp.aaKattioWithLineReader

// n is 2-200 inclusive, even
// m = connecting edges

let scanner = Scanner()
let n = scanner.NextInt()
let m = scanner.NextInt()

let listToStack lst =
    let stack = Stack<string>()
    for name in List.rev lst do
        printf $"Name: %s{name}\n"
        stack.Push name
    stack
    
let register () =
    let subject = scanner.Next()
    let prefsArr = scanner.NextLine()           // Scan pref list
    let prefsLst = Seq.toList prefsArr          // Convert to list
    let prefsStack = Stack<string>()            // Make stack for prefs
    for name in prefsLst |> List.rev do         // Fill stack in reverse order
        prefsStack.Push(name)
        
    let rec fillMap length (lst: string list) = // Remember priorities in map
        let rec aux acc index =
            match index with
            | i when i = length -> acc
            | i -> aux (Map.add lst[i] (i+1) acc) (i+1)
        aux Map.empty 0
        
    let prefsMap = fillMap prefsArr.Length prefsLst // Fill map
    
    (subject, prefsStack, prefsMap)

let rec readData (names, stacks, maps) count =
        match count with
        | 0 -> (names, stacks, maps)
        | _ ->
            let name, prefStack, prefMap = register()
            let updatedStacks = Map.add name prefStack stacks
            let updatedMaps = Map.add name prefMap maps
            let updatedNames = name::names
            readData (updatedNames, updatedStacks, updatedMaps) (count-1)

let loop =
    let names, stacks, maps = (readData (List.empty, Map.empty, Map.empty) n)
    // All data is now available with names being keys in either map.
    
    
    

let runStablePerfectMatch = loop