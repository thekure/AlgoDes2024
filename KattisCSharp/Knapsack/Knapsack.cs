namespace KattisCSharp.Knapsack;

using static System.Console;
using System.Collections.Generic;
using System.IO;
using System;

#pragma warning disable CS8602

public class Knapsack
{
    private static List<TestCase> ParseInput()
    {
        var cases = new List<TestCase>();
        
        using var reader = new StreamReader(OpenStandardInput());
        
        while (reader.Peek() != -1)
        {
            var line = reader.ReadLine().Split();
            var cap = int.Parse(line[0]);
            var num_objects = int.Parse(line[1]);
            var values = new int[num_objects];
            var weights = new int[num_objects];

            for (var i = 0; i < num_objects; i++)
            {
                var _line = reader.ReadLine().Split();
                
                var value = int.Parse(_line[0]);
                var weight = int.Parse(_line[1]);
                
                values[i] = value;
                weights[i] = weight;
            }
            
            cases.Add(new TestCase(cap, num_objects, values, weights));
        }

        return cases;
    }

    private static int Solve(int cap, int[] values, int[] weights, int[,] m, int i)
    {
        if(i == 0 || cap == 0) return 0;        // Base case
        if(m[i, cap] != -1) return m[i, cap];   // Check memory for previously calculated result

        if (weights[i - 1] > cap)
        {
            return m[i, cap] = Solve(cap, values, weights, m, i - 1);
        }

        var drop = Solve(cap,  values, weights, m, i - 1);;
        var take = values[i - 1] + Solve(cap - weights[i - 1],  values, weights, m, i - 1);
        
        return m[i, cap] = Math.Max(take, drop);
    }
    
    private static List<int> FindSelectedItems(int cap, int[] values, int[] weights, int[,] m)
    {
        var i = values.Length;  // Start from the last item
        var selectedItems = new List<int>();

        // If i is 0, there are no more items.
        // If cap is 0, we have found all the items in the sack.
        while (0 < i && 0 < cap)
        {
            // In the dp table, if the value of a cell differs from the one directly above it, the item in question was
            // taken. Otherwise the values would be equal.
            if (m[i, cap] != m[i - 1, cap]) 
            {
                selectedItems.Add(i);  // Save index
                cap -= weights[i - 1]; // Reduce capacity by the weight of the selected item
            }
            i--;  // Move to the previous item
        }

        return selectedItems;
    }

    private static void SolveTestCase(TestCase t)
    {
        int[,] _m;
        _m = new int[t.num_objects + 1, t.cap + 1];
        
        for (var i = 0; i < t.num_objects + 1; i++)
        {
            for (var j = 0; j < t.cap + 1; j++)
            {
                _m[i, j] = -1;
            }
        }
        
        // Find the highest possible value, but disregard the result
        Solve(t.cap, t.values, t.weights, _m, t.num_objects);
        
        // Find the actual objects that was part of the solution, by iterating over the dp table (memory)
        var indices = FindSelectedItems(t.cap, t.values, t.weights, _m);
        indices.Reverse();
        
        // Print the amount of selected objects
        WriteLine(indices.Count);
        foreach (var i in indices)
        {
            // Print the individual objects index separated by space
            Write($"{i-1} ");
        }
        // Newline after the last index
        WriteLine();
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