module KattisFSharp.tester

open KattisFSharp.aaKattio
open KattisFSharp.aaKattioWithLineReader

let scanner = Scanner()
let runTester =
    let line1 = scanner.NextLine()
    let line2 = scanner.NextLine()
    let line3 = scanner.NextLine()
    printf "["
    for i in line1 do printf $"%s{i}; "
    printf "]\n"
    printf "Line done\n"
    
    printf $"Array size: %i{line1.Length}\n"
    
    printf "["
    for i in line2 do printf $"%s{i}; "
    printf "]\n"
    printf "Line done\n"
    
    printf "["
    for i in line3 do printf $"%s{i}; "
    printf "]\n"
    printf "Line done\n"
    
    
