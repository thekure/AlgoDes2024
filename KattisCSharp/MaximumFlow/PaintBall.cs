using System.Net.Sockets;

namespace KattisCSharp.MaximumFlow;

using Kattis.IO;
using static System.Math;
using System.Collections.Generic;
using System;

#pragma warning disable CS8602
#pragma warning disable CS8618

public class PaintBall
{
    Scanner scanner;
    private MaxFlow_Gen flow;
    private int numPlayers;
    private int numLinesIn;
    private int start;
    private int end;
    List<int>[] shooters;




    public void Run()
    {
        scanner = new Scanner();
        
        numPlayers = scanner.NextInt(); // 2 <= n <= 1000
        numLinesIn = scanner.NextInt(); // 0 <= m <= 5000

        start = 0;
        end = numPlayers * 2 + 1;

        if (numLinesIn < (numPlayers / 2) - 1)
        {
            Console.WriteLine("Impossible"); return;
        }
        
        flow = new MaxFlow_Gen();
        flow.InitWithParams(numPlayers * 2 + 2, start, end);

        InitializePlayersAndTargets();
        AddEdgesFromStart();
        AddEdgesToEnd();
        AddPossibleShots();

        var maxFlow = flow.ComputeMaxFlow();
        
        if (maxFlow != numPlayers)
        {
            Console.WriteLine("Impossible"); return;
        }
        
        var finalTargets = flow.CollectForPaintBall(maxFlow, start, end, numPlayers);

        // Print results
        foreach (var target in finalTargets)
        {
            Console.WriteLine(target);
        }
    }

    private void AddPossibleShots()
    {
        for (var shooter = 1; shooter < numPlayers * 2 + 1; shooter++)
        {
            foreach (var target in shooters[shooter])
            {
                flow.AddEdge(shooter, target, 1);
            }
        }
    }

    private void AddEdgesFromStart()
    {
        for (var shooter = 1; shooter < numPlayers + 1; shooter++)
        {
            flow.AddEdge(start, shooter, 1);
        }

    }
    
    private void AddEdgesToEnd()
    {
        for (var target = 1; target < numPlayers + 1; target++)
        {
            flow.AddEdge(target + numPlayers, end, 1);
        }
    }

    private void InitializePlayersAndTargets()
    {
        shooters = new List<int>[(numPlayers * 2) + 1];
        
        for (var i = 1; i < numPlayers + 1; i++)
        {
            shooters[i] = new List<int>();
            shooters[i + numPlayers] = new List<int>();
        }

        for (var i = 1; i < numLinesIn + 1; i++)
        {
            var shooter = scanner.NextInt();
            var target = scanner.NextInt();
            
            shooters[shooter].Add(target + numPlayers);
            shooters[target].Add(shooter + numPlayers);
        }
    }
}