module KattisFSharp.nnBasicProgramming

open KattisFSharp.aaKattio

// first int:   N = array size
// second int:  t = what to do

let scanner = Scanner()
let n = scanner.NextInt()
let t = scanner.NextInt()
let a = Array.init n (fun _ -> scanner.NextInt())

let numberToChar n =
    char (n + int 'a')

let loop =
    match t with
        | 1 -> printf $"%i{7}"
        | 2 ->
            match a[0], a[1] with
            | a, b when a > b -> printf "Bigger"
            | a, b when a = b -> printf "Equal"
            | _ -> printf "Smaller"
        | 3 -> printf $"%i{(Array.sort a)[1]}"
        | 4 -> printf $"%i{(Array.sum a)}"
        | 5 -> printf $"%i{(Array.sum(Array.filter (fun elem -> elem % 2 = 0) a))}"
        | 6 ->
            let mod26 = Array.map (fun elem -> elem % 26) a
            let chars = Array.map numberToChar mod26
            printf $"{System.String(chars)}"
        | 7 ->
            let rec jump i' acc =
                match i', acc with
                | i, _ when i < 0 || i > n-1 -> printf "Out"
                | i, _ when i = n-1 -> printf "Done"
                | i, acc when Set.contains i acc -> printf "Cyclic"
                | i, _ -> jump a[i] (Set.add i acc)
            jump 0 Set.empty
        | _ -> ()
            
let runBasicProgramming = loop