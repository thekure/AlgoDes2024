using System.Diagnostics;

namespace KattisCSharp.MaximumFlow;
using System.Collections.Generic;
using Kattis.IO;
using System;



#pragma warning disable CS8602
#pragma warning disable CS8618


public class MaxFlow_EdKarp
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

    private long BFS()
    {
        var q = new Queue<int>(n);
        Mark(s);
        q.Enqueue(s);

        _path = new Edge[n];
        while (q.Count != 0)
        {
            var vertex = q.Dequeue();
            if(vertex == t) break;

            foreach (var edge in _graph[vertex])
            {
                var cap = edge.RemainingCap();
                if (cap <= 0 || IsMarked(edge.To)) continue; // break
                
                Mark(edge.To);
                _path[edge.To] = edge;
                q.Enqueue(edge.To);
            }
        }

        if (!IsMarked(t))
        {
            return 0;
        }
        var bottleNeck = long.MaxValue;

        for (var edge = _path[t]; edge != null; edge = _path[edge.From])
            bottleNeck = Math.Min(bottleNeck, edge.RemainingCap());

        for (var edge = _path[t]; edge != null; edge = _path[edge.From])
        {
            edge.Augment(bottleNeck);
        }
        
        return bottleNeck;
    }

    private void Solve() {
        //Stopwatch timer = Stopwatch.StartNew(); // Start timer for Solve
        long flow;
        do {
            ResetMarked();
            flow = BFS();
            _maxFlow += flow;
        } while (flow != 0);
        //timer.Stop(); // Stop timer for Solve
        //Console.WriteLine($"Solve took: {timer.ElapsedMilliseconds} ms"); // Log time taken
    }

    public void Run()
    {
        Init();
        Solve();
        
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
