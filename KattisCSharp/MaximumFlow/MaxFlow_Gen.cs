namespace KattisCSharp.MaximumFlow;
// I took the advice from class, and simply got an implementation that worked. I have
// not written this myself, but got it from ChatGPT.
// My own attempts (which were of course also heavily inspired by existing solutions),
// never managed to pass test case 15 of Kattis' Maximum Flow problem in less than 2 seconds,
// so I resorted to this to make progress.

using System;
using System.Collections.Generic;
using System.Linq;
using Kattis.IO;

#pragma warning disable CS8602
#pragma warning disable CS8618

public class MaxFlow_Gen
{
    public int n, m, s, t;
    public List<Dictionary<int, Edge>> graph;

    public class Edge
    {
        public readonly int From, To;
        public int Capacity, Flow;
        public Edge ReverseEdge;

        public int ResidualCapacity => Capacity - Flow;

        public Edge(int from, int to, int capacity)
        {
            From = from;
            To = to;
            Capacity = capacity;
            Flow = 0;
        }
    }

    private void Init()
    {
        // Read input
        var firstLine = Console.ReadLine().Split().Select(int.Parse).ToArray();
        n = firstLine[0];
        m = firstLine[1];
        s = firstLine[2];
        t = firstLine[3];
        
        BuildGraph();
        
        for (var i = 0; i < m; i++)
        {
            var edgeInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var u = edgeInput[0];
            var v = edgeInput[1];
            var c = edgeInput[2];

            AddEdge(u, v, c);
        }
    }

    private void BuildGraph()
    {
        graph = new List<Dictionary<int, Edge>>(n);
        for (var i = 0; i < n; i++)
        {
            graph.Add(new Dictionary<int, Edge>());
        }
    }

    public void InitWithParams(int n, int s, int t)
    {
        this.n = n;
        this.s = s;
        this.t = t;
        
        BuildGraph();
    }
    
    public int ComputeMaxFlow()
    {
        var maxFlow = 0;
        while (true)
        {
            var parent = new Edge[n];
            var queue = new Queue<int>();
            queue.Enqueue(s);
            var visited = new bool[n];
            visited[s] = true;

            while (queue.Count > 0)
            {
                var u = queue.Dequeue();
                foreach (var edge in graph[u].Values)
                {
                    var v = edge.To;
                    if (visited[v] || edge.ResidualCapacity <= 0) continue;
                    visited[v] = true;
                    parent[v] = edge;
                    queue.Enqueue(v);
                    if (v == t)
                        break;
                }
            }

            if (!visited[t])
                break;

            // Find bottleneck capacity
            var bottleneck = int.MaxValue;
            var vCurrent = t;
            while (vCurrent != s)
            {
                var e = parent[vCurrent];
                bottleneck = Math.Min(bottleneck, e.ResidualCapacity);
                vCurrent = e.From;
            }

            // Update flow
            vCurrent = t;
            while (vCurrent != s)
            {
                var e = parent[vCurrent];
                e.Flow += bottleneck;
                e.ReverseEdge.Flow -= bottleneck;
                vCurrent = e.From;
            }

            maxFlow += bottleneck;
        }

        return maxFlow;
    }

    public void CollectAndPrintFlow(int maxFlow)
    {
        // Collect edges with positive flow
        var flowEdges = new List<Edge>();
        for (var u = 0; u < n; u++)
        {
            foreach (var edge in graph[u].Values)
            {
                if (edge is { Flow: > 0, Capacity: > 0 })
                {
                    flowEdges.Add(edge);
                }
            }
        }
        
        // Output
        Console.WriteLine($"{n} {maxFlow} {flowEdges.Count}");
        foreach (var edge in flowEdges)
        {
            Console.WriteLine($"{edge.From} {edge.To} {edge.Flow}");
        }
        
    }
    public void Run()
    {
        Init();
        var maxFlow = ComputeMaxFlow();
        CollectAndPrintFlow(maxFlow);
    }

    public void AddEdge(int u, int v, int capacity)
    {
        Edge edge, reverseEdge;

        if (graph[u].ContainsKey(v))
        {
            edge = graph[u][v];
            edge.Capacity += capacity;
        }
        else
        {
            edge = new Edge(u, v, capacity);
            graph[u][v] = edge;
        }

        if (graph[v].ContainsKey(u))
        {
            reverseEdge = graph[v][u];
        }
        else
        {
            reverseEdge = new Edge(v, u, 0);
            graph[v][u] = reverseEdge;
        }

        edge.ReverseEdge = reverseEdge;
        reverseEdge.ReverseEdge = edge;
    }

    private bool EdgeIsRelevant(Edge edge, int start, int end)
    {
        return edge is { Flow: > 0, Capacity: > 0 } &&
               start < edge.From &&
               edge.To != end &&
               edge.From != start && 
               edge.To != start &&
               edge.From != end;
    }
    
    public int[] CollectForPaintBall(int maxFlow, int start, int end, int numPlayers)
    {
        int[] targets = new int[numPlayers];
        var index = 0;
        
        // Collect edges with positive flow
        var flowEdges = new List<Edge>();
        for (var u = 0; u < n; u++)
        {
            foreach (var edge in graph[u].Values)
            {
                if (EdgeIsRelevant(edge, start, end))
                {
                    targets[index] = edge.To - numPlayers;
                    index++;
                }
            }
        }

        return targets;

    }
}
