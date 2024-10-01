module KattisFSharp.nnMinimumCut

open KattisFSharp.aaKattio

(*
n: num_nodes   2 <= n <= 500
m: num_edges   0 <= m <= 10.000
s: source      0 <= s <= n - 1   &&   s != t
t: sink        0 <= t <= n - 1   &&   s != t

n m s t
4 5 0 3

There is an edge from u to v with weight w
u v w
0 1 10
1 2 1
1 3 1
0 2 1
2 3 10

Should output
2
1
0

Output should begin with a line containing an integer k, giving the size of U. 
Then follow k lines giving the vertices in U, one per line. If there are multiple 
choices for U any one will be accepted.

You may assume that there is a cut such that the total weight of edges from U to U'
is less than 2^31.

*)

let scanner = Scanner()

let n = scanner.NextInt()
let m = scanner.NextInt()
let s = scanner.NextInt()
let t = scanner.NextInt()

let readInput =
    let rec aux i acc =
        match i with
        | i when i = m -> acc |> List.rev
        | i ->
            let u = scanner.NextInt()
            let v = scanner.NextInt()
            let w = scanner.NextInt()
            aux (i+1) ((u, v, w) :: acc)
    aux 0 List.empty

let rec printInput input =
    match input with
    | (u, v, w) :: tail ->
        printf($"{u} {v} {w}\n")
        printInput tail
    | _ -> printf($"\n")


let loop =
    // 
    // printf($"n: {n}, m: {m}, s: {s}, t: {t}\n")
    let list = readInput
    // printInput list
    
    
    ()
let runMinimumCut = loop

