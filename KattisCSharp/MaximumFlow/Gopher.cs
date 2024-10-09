namespace KattisCSharp.MaximumFlow;

using Kattis.IO;
using static System.Math;
using System.Collections.Generic;
using System;

#pragma warning disable CS8602
#pragma warning disable CS8618

public class Gopher
{
    Scanner scanner;
    private List<Point> gophers;
    private List<Point> holes;
    private int idCount;
    private MaxFlow_Gen flow;
    

    public void Run()
    { 
        scanner = new Scanner();
        idCount = 0;
        
        // Keep reading from std in
        while (scanner.HasNext())
        {
            gophers = new List<Point>();
            holes   = new List<Point>();
            idCount = 0;
            
            var n = scanner.NextInt(); // gophers
            var m = scanner.NextInt(); // holes
            var s = scanner.NextInt(); // time limit (s)
            var v = scanner.NextInt(); // velocity (m/s)

            for (var i = 0; i < n; i++)
            {
                ReadGopher();
            }
            var lastGopherID = idCount;
            
            for (var i = 0; i < m; i++)
            {
                ReadHole();
            }
            var lastHoleID = idCount;
            
            flow = new MaxFlow_Gen();
            var start = idCount + 1;
            var end = idCount + 2;
            
            flow.InitWithParams(n+m+3, start, end);
            
            AddGopherEdges(start);
            AddHoleEdges(end);
            AddPossibleEscapeRoutes(s, v, lastGopherID, lastHoleID);

            var escapedGophers = flow.ComputeMaxFlow();
            var vulnerableGophers = lastGopherID - escapedGophers;
            
            Console.WriteLine(vulnerableGophers);
        }
    }

    private void AddPossibleEscapeRoutes(int time, int velocity, int lastGopherID, int lastHoleID)
    {
        var maxPossibleMovement = time * velocity;
        for (var i = 0; i < lastGopherID; i++)
        {
            var gopher = gophers[i];
            for (var j = 0; j < holes.Count; j++)
            {
                var hole = holes[j];
                var dist = gopher.Distance(hole);
                if (dist <= maxPossibleMovement)
                {
                    flow.AddEdge(gopher.ID, hole.ID, 1);
                }
            }
        }
    }
    private void AddGopherEdges(int start)
    {
        foreach (var gopher in gophers)
        {
            flow.AddEdge(start, gopher.ID, 1);
        }
    }
    private void AddHoleEdges(int end)
    {
        foreach (var hole in holes)
        {
            flow.AddEdge(hole.ID, end, 1);
        }
    }
    private void ReadGopher()
    {
        double x = scanner.NextDouble();
        double y = scanner.NextDouble();
        gophers.Add(new Point(x, y, idCount));
        idCount++;
    }
    private void ReadHole()
    {
        var x = scanner.NextDouble();
        var y = scanner.NextDouble();
        holes.Add(new Point(x, y, idCount));
        idCount++;
    }
    
}

public class Point
{
    private double X { get; }
    private double Y { get; }

    public int ID { get; }

    public Point(double x, double y, int id){
        X = x;
        Y = y;
        ID = id;
    }

    public double Distance(Point other){
        return Sqrt(Pow(X - other.X, 2) + Pow(Y - other.Y, 2));
    }
    
    public double Distance(double otherX, double otherY){
        return Sqrt(Pow(X - otherX, 2) + Pow(Y - otherY, 2));
    }
}