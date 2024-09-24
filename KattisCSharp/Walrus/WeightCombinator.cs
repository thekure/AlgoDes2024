namespace KattisCSharp.Walrus;

#pragma warning disable CS8618
#pragma warning disable CS0414

using static System.Console;
using static System.StringSplitOptions;


public class WeightCombinator
{
    private int n;
    private const int target = 1000;
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
                Write(_m[i, j] == -1 ? "." : $"{_m[i, j]} ");
            }
            WriteLine();
        }
    }
    
    private static int GetDist(int val)
    {
        return int.Abs(target - val);
    }
    
    private static int ChooseBest(int drop, int take)
    {
        var dropDist = GetDist(drop);
        var takeDist = GetDist(take);

        return takeDist <= dropDist ? take : drop;
    }
    
    private int Solve(int curDist, int m)
    {
        if(m == 0 || curDist <= 0) return target - curDist;
        if(_m[m, curDist] != -1) return _m[m, curDist];      // Check memory for previously calculated result
        
        var drop = Solve(curDist, m - 1);
        var take = Solve(curDist - _weights[m-1], m - 1);
        
        var optimal = ChooseBest(drop, take);

        return _m[m, curDist] = optimal;
    }
    
    public void Run()
    {
        ParseInput();
        if      (_weights.Length == 1)    WriteLine(_weights[0]);
        else if (_weights.Contains(target)) WriteLine(target);
        else
        {
            var result = Solve(target, n);
            WriteLine(result);
        }
    }
}