using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace KattisCSharp.Walrus;

#pragma warning disable CS8618
#pragma warning disable CS0414

using static System.Console;
using static System.StringSplitOptions;


public class WeightCombinator
{
    private int n;
    private const int _weightLimit = 50;
    private int[] _weights;
    private int[,] _m;

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
        
        Array.Sort(_weights);
        // Array.Sort(_weights, (a, b) => b.CompareTo(a));
    }
    
    private void InitializeGlobals()
    {
        _weights = new int[n];
        _m = new int[n+1, _weightLimit+1];
        for (var i = 0; i < n+1; i++)
        {
            for (var j = 0; j < _weightLimit+1; j++)
            {
                _m[i, j] = -1;
            }
        }
    }

    private void PrintWeights()
    {
        foreach (var w in _weights) {
            WriteLine(w);
        }
    }
    private void PrintM()
    {
        for (var i = 0; i < n; i++)
        {
            Write($"{i}: ");
            for (var j = 0; j < _weightLimit; j++)
            {
                Write(_m[i, j] == -1 ? "_ " : $"{_m[i, j]} ");
            }
            WriteLine();
        }
    }
    
    private static int GetDist(int val)
    {
        return int.Abs(_weightLimit - val);
    }

    private void Indent(int i)
    {
        for (var j = 0; j < (-i) + n; j++)
        {
            Write(" ");
        }
    }
    private int Solve(int cap, int m)
    {
        if(m == 0 || cap == 0) return 0;        // Base case
        if(_m[m, cap] != -1) return _m[m, cap]; // Check memory for previously calculated result
        
        if (_weights[m - 1] > cap)
        {
            return _m[m, cap] = Solve(cap,  m - 1);
        }

        return _m[m, cap] = 
            Math.Max(_weights[m - 1] + Solve(cap - _weights[m - 1], m - 1),
                Solve(cap, m - 1));
    }
    
    public void Run()
    {
        ParseInput();
        if      (_weights.Length == 1)    WriteLine(_weights[0]);
        else if (_weights.Contains(_weightLimit)) WriteLine(_weightLimit);
        else
        {
            var result = Solve(_weightLimit, n);
            PrintM();
            WriteLine();
            WriteLine($"Solution: {result}");
            WriteLine();
        }
    }
}