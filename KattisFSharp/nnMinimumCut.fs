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

// Queue data structure borrowed from this post:
// https://stackoverflow.com/questions/33464319/implement-a-queue-type-in-f
type queue<'a> =
    | Queue of 'a list * 'a list


type Edge = {
    From: int
    To: int
    Capacity: int
    Backwards: int
}
let newEdge from _to cap backwards = { From = from; To = _to; Capacity = cap; Backwards = backwards }
let updatedEdge edge cap = { edge with Capacity = cap }

let empty = Queue([], [])

let enqueue q e = 
    match q with
    | Queue(fs, bs) -> Queue(e :: fs, bs)

let dequeue q = 
    match q with
    | Queue([], []) -> -1, empty
    | Queue(fs, b :: bs) -> b, Queue(fs, bs)
    | Queue(fs, []) -> 
        let bs = List.rev fs
        bs.Head, Queue([], bs.Tail)

let readInput =
    let rec aux i (edges, adj, forward, backward) =
        match i with
        | i when i = m ->
            let newLst = List.rev edges
            newLst, adj, forward, backward
        | i ->
            let u = scanner.NextInt()
            let v = scanner.NextInt()
            let c = scanner.NextInt()
            
            // Update edges list
            let newEdges = (u, v, c) :: edges
            
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
            let newForwardLst = (v, c) :: forwardLst              // Add v to u's forward list
            let newForward = Map.add u newForwardLst forward      // Update forward map
            
            // Update backward residual map
            let backwardLst =                                     // Find backward list for vertex v
                match Map.tryFind v backward with
                | Some list -> list
                | None -> List.empty
            let newBackwardLst = (u, 0) :: backwardLst            // Add u to v's backward list with c 0
            let newBackward = Map.add v newBackwardLst backward   // Update backward map
            
            aux (i+1) (newEdges, newAdj, newForward, newBackward) // Continue reading
    aux 0 (List.empty, Map.empty, Map.empty, Map.empty)

let readInputEdgeEdition =
    let rec aux i edges =
        match i with
        | i when i = m ->
            List.rev edges
        | i ->
            let u = scanner.NextInt()
            let v = scanner.NextInt()
            let c = scanner.NextInt()
            
            // Update edges list
            let edge = newEdge u v c 0
            let newEdges = edge :: edges
            
            aux (i+1) newEdges // Continue reading
    aux 0 List.empty

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

let marks : bool array = Array.zeroCreate n

let mark v = marks.[v] <- true

let isMarked v = marks.[v]


let bfs graph =
    mark s                         // mark start vertex
    let q = enqueue empty s        // initialize q
    let path = Array.zeroCreate m  // array to keep track of the final path from t to s
    
    let rec search q path =
        match dequeue q with
        | -1, empty                -> path
        | value, q' when value = t -> path
        | value, q' ->
            let rec iterateGraph edges =
                match edges with
                | head :: tail ->
                    
            iterateGraph graph
            // for each edge in graph
            // get remaining capacity
            // if 0 < cap && 
    
    let result = search q path
    ()

let loop =
    let graph = readInputEdgeEdition
    // printf($"n: {n}, m: {m}, s: {s}, t: {t}\n")
    // printEdges edges
    // printAdjList (Map.find 1 adj)
    // printResList (Map.find 1 forward)
    // printResList (Map.find 2 backward)

    
    
    
    ()
    
let runMinimumCut = loop

