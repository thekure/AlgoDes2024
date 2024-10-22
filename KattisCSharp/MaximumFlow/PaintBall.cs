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

        for (int i = 1; i < shooters.Length; i++)
        {
            Console.Write($"Shooter #{i} can shoot ");
            foreach (var target in shooters[i])
            {
                Console.Write($"{target} ");
            }
            Console.WriteLine();
        }
        
        AddEdgesFromStart();
        AddEdgesToEnd();
        AddPossibleShots();

        var result = flow.ComputeMaxFlow();
        
        Console.WriteLine($"Number of players: {numPlayers}");
        Console.WriteLine($"Max Flow: {result}");
        // Console.WriteLine(result);
        
        flow.CollectAndPrintFlow(result);
    }

    private void AddPossibleShots()
    {
        Console.WriteLine($"\nAddPossibleShots");
        for (var shooter = 1; shooter < numPlayers * 2 + 1; shooter++)
        {
            Console.WriteLine($"Current shooter: {shooter}");
            foreach (var target in shooters[shooter])
            {
                Console.WriteLine($"Adding edge from {shooter} to {target}");
                flow.AddEdge(shooter, target, 1);
            }
        }
        
        Console.WriteLine();
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
        Console.WriteLine($"\nAddEdgesToEnd");
        for (var target = 1; target < numPlayers + 1; target++)
        {
            Console.WriteLine($"Adding edge from {target + numPlayers} to {end}");
            flow.AddEdge(target + numPlayers, end, 1);
        }
    }

    private void InitializePlayersAndTargets()
    {
        shooters = new List<int>[(numPlayers * 2) + 1];
        
        Console.WriteLine($"Shooters can hold {numPlayers * 2}");
        Console.WriteLine();
        
        for (var i = 1; i < numPlayers + 1; i++)
        {
            Console.WriteLine($"Shooter {i} is getting a list of targets.");
            shooters[i] = new List<int>();
            Console.WriteLine($"Shooter {i + numPlayers} is getting a list of targets.");
            shooters[i + numPlayers] = new List<int>();
        }
        
        Console.WriteLine();
        
        for (var i = 1; i < numLinesIn + 1; i++)
        {
            var shooter = scanner.NextInt();
            var target = scanner.NextInt();
            
            Console.WriteLine($"Current shooter: {shooter}");
            Console.WriteLine($"Current target: {target}");
            
            shooters[shooter].Add(target + numPlayers);
            Console.WriteLine($"Shooter {shooter} added target {target + numPlayers}");
            shooters[target].Add(shooter + numPlayers);
            Console.WriteLine($"Shooter {target} added target {shooter + numPlayers}");
        }
        Console.WriteLine();
    }
}