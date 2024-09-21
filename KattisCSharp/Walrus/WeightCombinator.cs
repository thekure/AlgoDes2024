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
    private const int target = 50;
    private int[] _weights;
    private int[,] _m;
    private int total;

    private void ParseInput()
    {
        using var reader = new StreamReader(OpenStandardInput());
        var input = reader.ReadToEnd();
        var lines = input.Split(new[] { '\n' }, RemoveEmptyEntries);

        n = int.Parse(lines[0]);
        InitializeGlobals();

        for (var i = 1; i < n+1; i++) {
            _weights[i-1] = int.Parse(lines[i]);
        }
        
        Array.Sort(_weights);
    }
    
    private void InitializeGlobals()
    {
        _weights = new int[n];
        _m = new int[n+1, target*2+1];
        for (var i = 0; i < n+1; i++)
        {
            for (var j = 0; j < target*2+1; j++)
            {
                _m[i, j] = -1;
            }
        }
    }
    
    private void PrintM()
    {
        for (var i = 0; i < n+1; i++)
        {
            Write($"{i}: ");
            for (var j = 0; j < target*2+1; j++)
            {
                Write(_m[i, j] == -1 ? "_ " : $"{_m[i, j]} ");
            }
            WriteLine();
        }
    }
    
    private static int GetDist(int val)
    {
        return int.Abs(target - val);
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

        var drop = Solve(cap, m - 1);
        var take = _weights[m - 1] + Solve(cap - _weights[m - 1], m - 1);
        Indent(m);
        Write($"{m}: ");
        Indent(m);
        WriteLine($"drop: {drop}, take: {take}");
        return _m[m, cap] = 
            Math.Max(take, drop);
    }
    
    public void Run()
    {
        ParseInput();
        if      (_weights.Length == 1)    WriteLine(_weights[0]);
        else if (_weights.Contains(target)) WriteLine(target);
        else
        {
            var result = Solve(target, n);
            PrintM();
            WriteLine();
            WriteLine($"Solution: {result}");
            WriteLine();
        }
    }
}