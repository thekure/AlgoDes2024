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
        InitializeGlobals();

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

        int drop;
        int take;
        if (_results[index] == -1)
        {
            drop = Solve(index + 1);
            _results[index] = drop;
        }
        else
        {
            drop = _results[index];
        }

        if (_results[index] == -1)
        {
            take = Solve(index + 1);
            _results[index] = take;
            take = _weights[index] + take;
        }
        else
        {
            take = _weights[index] + _results[index];
        }
        
        return GetDist(drop) < GetDist(take) ? drop : take;
    }
    
    public void Run()
    {
        ParseInput();
        // PrintWeights();
        if      (_weights.Length == 1)    WriteLine(_weights[0]);
        else if (_weights.Contains(1000)) WriteLine(1000);
        else
        {
            var result = Solve();
            WriteLine(Solve());
        }
    }
}