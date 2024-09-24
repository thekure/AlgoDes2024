namespace KattisCSharp.Knapsack;

using static Console;

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
        
        WriteLine(Solve(t.cap, t.values, t.weights, _m, t.num_objects));
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