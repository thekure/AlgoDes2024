using System.Diagnostics;

namespace KattisCSharp.MaximumFlow;
using System.Collections.Generic;
using Kattis.IO;
using System;



#pragma warning disable CS8602
#pragma warning disable CS8618


public class MaxFlow_Dinic
{
    private int n;
    private int m;
    private int s;
    private int t;

    private long _maxFlow;

    private List<Edge>[] _graph;
    private Edge[] _path;
    private int[] _marked;

    private int _markerToken = 1;

    private int[] _level;
    private int[] _ptr;
    
    private static long INF = long.MaxValue / 2;

    private class Edge
    {
        public readonly int From;
        public readonly int To;
        private readonly long _capacity;
        public long Flow;
        public Edge Residual;

        public Edge(int from, int to, long capacity)
        {
            From = from;
            To = to;
            _capacity = capacity;
            Flow = 0;
        }

        public long RemainingCap()
        {
            return _capacity - Flow;
        }

        public void Augment(long bottleNeck)
        {
            Flow += bottleNeck;
            Residual.Flow -= bottleNeck;
        }
    }

    private void ResetMarked()
    {
        _markerToken++;
    }

    private void Mark(int vertex)
    {
        _marked[vertex] = _markerToken;
    }

    private bool IsMarked(int vertex)
    {
        return _marked[vertex] == _markerToken;
    }

    private void Init()
    {
        // Stopwatch timer = Stopwatch.StartNew(); // Start timer for input reading

        var scanner = new Scanner();
        n = scanner.NextInt();  // number of nodes
        m = scanner.NextInt();  // number of edges
        s = scanner.NextInt();  // source node
        t = scanner.NextInt();  // target node
        
        _graph = new List<Edge>[n];
        _marked = new int[n];
        _ptr = new int[n];
        _level = new int[n];
        
        ResetMarked();

        for (var i = 0; i < n; i++)
        {
            _graph[i] = new List<Edge>();
        }
        
        for (var i = 0; i < m; i++)
        {
            // Read the u, v, and c values from input
            var from = scanner.NextInt();
            var to = scanner.NextInt();
            var cap = scanner.NextLong();

            var edge = new Edge(from, to, cap);
            var res = new Edge(to, from, 0);
            edge.Residual = res;
            res.Residual = edge;

            // Add the new edge (u, v, c) to the list
            _graph[from].Add(edge);
            _graph[to].Add(res);
        }
        // timer.Stop(); // Stop timer for input reading
        // Console.WriteLine($"Input reading took: {timer.ElapsedMilliseconds} ms"); // Log time taken
    }

    private bool BFS()
    {
        var q = new Queue<int>(n);
        q.Enqueue(s);
        Array.Fill(_level, -1);
        _level[s] = 0;

        _path = new Edge[n];
        
        while (q.Count != 0)
        {
            var vertex = q.Dequeue();

            foreach (var edge in _graph[vertex])
            {
                var cap = edge.RemainingCap();
                if (0 < cap && _level[edge.To] < 0){
                    _level[edge.To] = _level[vertex] + 1;
                    q.Enqueue(edge.To);
                }
            }
        }
        return _level[t] >= 0;
    }

    private void Solve() {
        //Stopwatch timer = Stopwatch.StartNew(); // Start timer for Solve
        while (BFS())
        {
            Array.Fill(_ptr, 0);
            long flow;
            while ((flow = DFS(s, INF)) != 0)
            {
                _maxFlow += flow;
            }
        }
    }

    private long DFS(int vertex, long flow)
    {
        if (vertex == t || flow == 0) return flow;
        for (; _ptr[vertex] < _graph[vertex].Count; _ptr[vertex]++)
        {
            var edge = _graph[vertex][_ptr[vertex]];
            if (_level[edge.To] == _level[vertex] + 1 && 0 < edge.RemainingCap())
            {
                long bottleNeck = DFS(edge.To, Math.Min(flow, edge.RemainingCap()));
                if (0 < bottleNeck)
                {
                    edge.Augment(bottleNeck);
                    return bottleNeck;
                }
            }
        }

        return 0;
    }

    public void Run()
    {
        Init();
        Solve();
        Console.WriteLine(_maxFlow);
        
        var writer = new BufferedStdoutWriter();
        
        //Stopwatch timer = Stopwatch.StartNew(); // Start timer for Solve
        var printer = new List<string>();
        foreach (var lst in _graph)
        {
            foreach (var edge in lst)
            {
                if(edge.Flow > 0) printer.Add("" + edge.From + " " + edge.To + " " + edge.Flow);
            }
        }

        //timer.Stop(); // Stop timer for Solve
        //Console.WriteLine($"Finding all prints took: {timer.ElapsedMilliseconds} ms"); // Log time taken

        // Output results using BufferedStdoutWriter
        writer.WriteLine($"{_path.Length} {_maxFlow} {printer.Count}");
        foreach (var s in printer)
        {
            writer.WriteLine(s);
        }
        writer.Flush();  // Flush the writer to ensure output is written
    }
    
    /*public static void Main(string[] args)
    {
        var maxFlow = new MaxFlow_EdKarp();
        maxFlow.Run();
    }*/
}
