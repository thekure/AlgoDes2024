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
    let rec aux i (edges, adj, forward, backward) =
        match i with
        | i when i = m ->
            let newLst = List.rev edges
            newLst, adj, forward, backward
        | i ->
            let u = scanner.NextInt()
            let v = scanner.NextInt()
            let w = scanner.NextInt()
            
            // Update edges list
            let newEdges = (u, v, w) :: edges
            
            // Update adjacency map (might become obsolete)
            let adjLst =                         // Find adjacency list for vertex u
                match Map.tryFind u adj with
                | Some list -> list
                | None -> List.empty
            let newAdjLst = v :: adjLst          // Add v to u's adj list    
            let newAdj = Map.add u newAdjLst adj // Update adjacency map
            
            // Update forward residual map
            let forwardLst =                                      // Find forward list for vertex u 
                match Map.tryFind u forward with
                | Some list -> list
                | None -> List.empty
            let newForwardLst = (v, w) :: forwardLst              // Add v to u's forward list
            let newForward = Map.add u newForwardLst forward      // Update forward map
            
            // Update backward residual map
            let backwardLst =                                     // Find backward list for vertex v
                match Map.tryFind v backward with
                | Some list -> list
                | None -> List.empty
            let newBackwardLst = (u, 0) :: backwardLst            // Add u to v's backward list with w 0
            let newBackward = Map.add v newBackwardLst backward   // Update backward map
            
            aux (i+1) (newEdges, newAdj, newForward, newBackward) // Continue reading
    aux 0 (List.empty, Map.empty, Map.empty, Map.empty)

let rec printEdges input =
    match input with
    | (u, v, w) :: tail ->
        printf($"{u} {v} {w}\n")
        printEdges tail
    | _ -> printf($"\n")

let rec printAdjList lst =
    printf($"[ ")
    let rec aux lst =
        match lst with
        | v :: tail ->
            printf($"{v} ")
            aux tail
        | _ -> printf($"]\n")
    aux lst

let rec printResList lst =
    printf($"[ ")
    let rec aux lst =
        match lst with
        | (vertex, weight) :: tail ->
            printf($"({vertex}, {weight}) ")
            aux tail
        | _ -> printf($"]\n")
    aux lst

let loop =
    // 
    // printf($"n: {n}, m: {m}, s: {s}, t: {t}\n")
    let edges, adj, forward, backward = readInput
    // printEdges edges
    // printAdjList (Map.find 1 adj)
    // printResList (Map.find 1 forward)
    // printResList (Map.find 2 backward)
    
    
    ()
let runMinimumCut = loop

