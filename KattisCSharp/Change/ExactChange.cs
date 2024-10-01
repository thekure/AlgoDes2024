using System.Runtime.CompilerServices;

namespace KattisCSharp.Change;

using static System.Console;
using System.Collections.Generic;
using System.IO;
using System;

#pragma warning disable CS8602
#pragma warning disable CS8604

public static class ExactChange
{
    private static int num_cases;
    private static List<TestCaseChange> ParseInput()
    {
        var cases = new List<TestCaseChange>();
        using var reader = new StreamReader(OpenStandardInput());
        ExactChange.num_cases = int.Parse(reader.ReadLine());

        for (var i = 0; i < num_cases; i++)
        {
            var price = int.Parse(reader.ReadLine());
            var num_moneys = int.Parse(reader.ReadLine());
            var denoms = new int[num_moneys];

            for (var j = 0; j < num_moneys; j++)
            {
                denoms[j] = int.Parse(reader.ReadLine());
            }
            Array.Sort(denoms);
            cases.Add(new TestCaseChange(price, num_moneys, denoms));
        }

        return cases;
    }

    private static void PrintM(int[,] m)
    {
        for (var i = 0; i < num_cases + 1; i++)
        {
            Write($"{i}: ");
            for (var j = 0; j < 10000 + 1; j++)
            {
                Write(m[i, j] == -1 ? "" : $"{m[i, j]} ");
            }
            WriteLine();
        }
    }
    private static int ChooseBest(int drop, int take, int price)
    {
        if (drop == price) {return drop;}
        if (take == price) return take;
        if (price < drop && price < take) return Math.Min(drop, take);
        if (drop < price && take < price) return Math.Max(drop, take);
        if (price < drop && take < price) return drop;
        if (price < take && drop < price) return take;
        
        WriteLine($"Case not handled: drop: {drop}, take: {take}, price: {price}");
        return -1;
    }
    
    private static List<int> FindDenomCount(int result, int[] denoms, int[,] m)
    {
        var i = denoms.Length;  // Start from the last item
        // WriteLine($"Start i: {i}");
        var selectedItems = new List<int>();

        // If i is 0, there are no more items.
        // If cap is 0, we have found all the items in the sack.
        while (0 < i && 0 < result)
        {
            // WriteLine($"i: {i}, result: {result}");
            // In the dp table, if the value of a cell differs from the one directly above it, the item in question was
            // taken. Otherwise the values would be equal.
            if (m[i, result] != m[i - 1, result]) 
            {
                selectedItems.Add(i);  // Save index
                result -= denoms[i - 1]; // Reduce capacity by the weight of the selected item
            }
            i--;  // Move to the previous item
        }

        return selectedItems;
    }
    
    private static int Solve(int price, int[] denoms, int[,] m, int i, int remaining, int[,] coin_count)
    {
        if(i == 0 || remaining <= 0) return price - remaining;          
        if(m[i, remaining] != -1) return m[i, remaining];           // Check memory for previously calculated result
        
        var drop = Solve(price, denoms, m, i - 1, remaining, coin_count);;
        var take = Solve(price,  denoms, m, i - 1, remaining - denoms[i - 1], coin_count);
        
        var optimal = ChooseBest(drop, take, price);
        
        // Track the coin count:
        if (optimal == take)  // If we took the current coin
        {
            coin_count[i-1, remaining] = 1 + coin_count[i - 1, remaining - denoms[i - 1]];
        }
        else  // If we didn't take the current coin
        {
            coin_count[i-1, remaining] = coin_count[i - 1, remaining];
        }
        
        return m[i, remaining] = optimal;
    }

    private static void SolveTestCase(TestCaseChange t)
    {
        int[,] _m;
        var coin_count = new int[t.num_moneys + 1, t.price + 1];
        
        _m = new int[t.num_moneys + 1, 10000 + 1];
        
        for (var i = 0; i < t.num_moneys + 1; i++)
        {
            for (var j = 0; j < 10000 + 1; j++)
            {
                _m[i, j] = -1;
            }
        }
        
        var result = Solve(t.price, t.denoms, _m, t.num_moneys, t.price, coin_count);
        int coinsUsed = coin_count[t.num_moneys, t.price];
        
        
        WriteLine();
        WriteLine(coinsUsed);
        
        WriteLine(result);
    }
    
    public static void Run()
    {
        var cases = ParseInput();
        foreach (var c in cases)
        {
            SolveTestCase(c);
        }
    }
}