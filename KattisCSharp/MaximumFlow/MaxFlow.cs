namespace KattisCSharp.MaximumFlow;
using System.Collections.Generic;
using System;

#pragma warning disable CS8602
#pragma warning disable CS8618

// My solution is inspired by the this implementation: https://github.com/williamfiset/Algorithms/blob/master/src/main/java/com/williamfiset/algorithms/graphtheory/networkflow/examples/EdmondsKarpExample.java#L81
public class MaxFlow
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
        var nums = Console.ReadLine().Split();
        n = int.Parse(nums[0]);  // number of nodes
        m = int.Parse(nums[1]);  // number of edges
        s = int.Parse(nums[2]);  // source node
        t = int.Parse(nums[3]);  // target node
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
            var input = Console.ReadLine().Split();
            
            var from = int.Parse(input[0]);
            var to = int.Parse(input[1]);
            var cap = long.Parse(input[2]);

            var edge = new Edge(from, to, cap);
            var res = new Edge(to, from, 0);
            edge.Residual = res;
            res.Residual = edge;
            
            // Add the new edge (u, v, c) to the list
            _graph[from].Add(edge);
            _graph[to].Add(res);
        }
        
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

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (!IsMarked(t))
        {
            return 0;
        }
        var bottleNeck = long.MaxValue;
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        for (var edge = _path[t]; edge != null; edge = _path[edge.From])
            bottleNeck = Math.Min(bottleNeck, edge.RemainingCap());
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        for (var edge = _path[t]; edge != null; edge = _path[edge.From])
        {
            edge.Augment(bottleNeck);
        }
        
        return bottleNeck;
    }
    
    private void Solve() {
        long flow;
        do {
            ResetMarked();
            flow = BFS();
            _maxFlow += flow;
        } while (flow != 0);
    }
    public void Run()
    {
        Init();
        Solve();

        var printer = new List<string>();
        foreach (var lst in _graph)
        {
            foreach (var edge in lst)
            {
                if(edge.Flow > 0) printer.Add("" + edge.From + " " + edge.To + " " + edge.Flow);
            }
        }
        
        Console.WriteLine($"{_path.Length} {_maxFlow} {printer.Count}");
        foreach (var s in printer)
        {
            Console.WriteLine(s);
        }
    }
}