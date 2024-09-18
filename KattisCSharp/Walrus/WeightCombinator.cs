using System.Runtime.InteropServices;

namespace KattisCSharp.Walrus;

#pragma warning disable CS8618
#pragma warning disable CS0414

using static System.Console;
using static System.StringSplitOptions;


public class WeightCombinator
{
    private int n;
    private int[] _weights;
    private int[] _results;
    private int _bestDistance = int.MaxValue;
    private int _bestCombination = 0;

    private void ParseInput()
    {
        using var reader = new StreamReader(OpenStandardInput());
        var input = reader.ReadToEnd();
        var lines = input.Split(new[] { '\n' }, RemoveEmptyEntries);

        n = int.Parse(lines[0]);
        

        for (var i = 1; i < n+1; i++) {
            _weights[i-1] = (int.Parse(lines[i]));
        }
        
        Array.Sort(_weights, (a, b) => b.CompareTo(a));
    }
    
    private void InitializeGlobals()
    {
        _weights = new int[n];
        _results = new int[n];
        Array.Fill(_results, -1);
    }

    private void PrintWeights()
    {
        foreach (var w in _weights) {
            WriteLine(w);
        }
    }

    private int NewDist(int combo, int index)
    {
        return int.Abs(1000 - (_weights[index] + combo));
    }
    
    private static int GetDist(int val)
    {
        return int.Abs(1000 - val);
    }
    
    private int Solve(int index = 0)
    {
        if (_weights.Length == index) return 0;
        if (_weights.Length == 1) return _weights[0];
        
        var drop = Solve(index + 1);
        var take = _weights[index] + Solve(index + 1);
        if(index == 0) WriteLine($"take w i = 0: {take}, drop: {drop}");
        return GetDist(drop) < GetDist(take) ? drop : take;
    }
    
    /*private int Solve(int distance = 1000, int combo = 0, int index = 0)
    {
        if (_weights.Length == index) {
            WriteLine("No more weights."); // base case
            WriteLine();
            return 0; 
        }
        WriteLine($"Distance: {distance}, Combo: {combo}, Index: {index}, Current element: {_weights[index]}");

        
        // Drop case
        if (NewDist(combo, index) > distance)
        {
            WriteLine($"Drop Case accepted - NewDist: {NewDist(combo, index)}\n");
            return Solve(distance, combo, index + 1);
        }
        
        // Take case
        var newDist = NewDist(combo, index);
        var newCombo = combo + _weights[index];
        WriteLine($"newDist: {newDist}, newCombo: {newCombo}");

        WriteLine("End");
        WriteLine();
        return Math.Max(
            Solve(newDist, newCombo, index + 1),
            _weights[index] + Solve(newDist, newCombo, index + 1)
        );
        
    }*/

    public void Run()
    {
        ParseInput();
        InitializeGlobals();
        PrintWeights();
        if      (_weights.Length == 1)    WriteLine(_weights[0]);
        else if (_weights.Contains(1000)) WriteLine(1000);
        else
        {
            var result = Solve();
            WriteLine(Solve());
        }
    }
}