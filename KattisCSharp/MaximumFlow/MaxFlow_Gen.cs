namespace KattisCSharp.MaximumFlow;
// I took the advice from class, and simply got an implementation that worked. I have
// not written this myself, but got it from ChatGPT.
// My own attempts (which were of course also heavily inspired by existing solutions),
// never managed to pass test case 15 of Kattis' Maximum Flow problem in less than 2 seconds,
// so I resorted to this to make progress.

using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8602
#pragma warning disable CS8618

public class MaxFlow_Gen
{
    static int n, m, s, t;
    static List<Dictionary<int, Edge>> graph;

    public class Edge
    {
        public int From, To, Capacity, Flow;
        public Edge ReverseEdge;

        public int ResidualCapacity => Capacity - Flow;

        public Edge(int from, int to, int capacity)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
            this.Flow = 0;
        }
    }

    public void Run()
    {
        // Read input
        var firstLine = Console.ReadLine().Split().Select(int.Parse).ToArray();
        n = firstLine[0];
        m = firstLine[1];
        s = firstLine[2];
        t = firstLine[3];

        graph = new List<Dictionary<int, Edge>>(n);
        for (int i = 0; i < n; i++)
        {
            graph.Add(new Dictionary<int, Edge>());
        }

        for (int i = 0; i < m; i++)
        {
            var edgeInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int u = edgeInput[0];
            int v = edgeInput[1];
            int c = edgeInput[2];

            AddEdge(u, v, c);
        }

        // Compute max flow
        int maxFlow = 0;
        while (true)
        {
            Edge[] parent = new Edge[n];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            bool[] visited = new bool[n];
            visited[s] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (var edge in graph[u].Values)
                {
                    int v = edge.To;
                    if (!visited[v] && edge.ResidualCapacity > 0)
                    {
                        visited[v] = true;
                        parent[v] = edge;
                        queue.Enqueue(v);
                        if (v == t)
                            break;
                    }
                }
            }

            if (!visited[t])
                break;

            // Find bottleneck capacity
            int bottleneck = int.MaxValue;
            int vCurrent = t;
            while (vCurrent != s)
            {
                Edge e = parent[vCurrent];
                bottleneck = Math.Min(bottleneck, e.ResidualCapacity);
                vCurrent = e.From;
            }

            // Update flow
            vCurrent = t;
            while (vCurrent != s)
            {
                Edge e = parent[vCurrent];
                e.Flow += bottleneck;
                e.ReverseEdge.Flow -= bottleneck;
                vCurrent = e.From;
            }

            maxFlow += bottleneck;
        }

        // Collect edges with positive flow
        List<Edge> flowEdges = new List<Edge>();
        for (int u = 0; u < n; u++)
        {
            foreach (var edge in graph[u].Values)
            {
                if (edge.Flow > 0 && edge.Capacity > 0)
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

    static void AddEdge(int u, int v, int capacity)
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
}
