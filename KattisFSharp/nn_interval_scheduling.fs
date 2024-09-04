module KattisFSharp.nn_interval_scheduling

open KattisFSharp.aaKattio

let scanner = Scanner()
let n = scanner.NextInt()

let readInput =
    let rec aux i acc = 
        match i with
        | i when i = n -> acc |> List.rev
        | i -> aux (i+1) ((scanner.NextInt(), scanner.NextInt())::acc)
    aux 0 List.empty
    
let sortByFinish lst =
    List.sortBy snd lst
    
let fillSet lst =
    let rec aux lst (acc, max) =
        match lst with
        | [] -> acc
        | (s,f) :: tail when s >= max ->
            aux tail ((Set.add (s,f) acc), f)
        | _ :: tail -> aux tail (acc,max)
    aux lst (Set.empty, 0)

let loop =
    let jobs = readInput
    let sortedJobs = sortByFinish jobs
    let scheduled_set = fillSet sortedJobs
    let answer = Set.count scheduled_set
    printf $"{answer}\n"
    
   
    ()
    
let runInternalScheduling = loop