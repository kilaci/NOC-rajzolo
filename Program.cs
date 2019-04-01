using System;
using System.Collections.Generic;
using System.Linq;






public static class Dijkstra
{
    public static List<int> DijkstraAlg(int[,] graph, int sourceNode, int destinationNode)
    {
        var n = graph.GetLength(0);
        var distance = new int[n];
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }
        distance[sourceNode] = 0;

        var used = new bool[n];
        var previous = new int?[n];

        while (true)
        {
            var minDistance = int.MaxValue;
            var minNode = 0;
            for (int i = 0; i < n; i++)
            {
                if (!used[i] && minDistance > distance[i])
                {
                    minDistance = distance[i];
                    minNode = i;
                }
            }

            if (minDistance == int.MaxValue)
            {
                break;
            }

            used[minNode] = true;

            for (int i = 0; i < n; i++)
            {
                if (graph[minNode, i] > 0)
                {
                    var shortestToMinNode = distance[minNode];
                    var distanceToNextNode = graph[minNode, i];

                    var totalDistance = shortestToMinNode + distanceToNextNode;

                    if (totalDistance < distance[i])
                    {
                        distance[i] = totalDistance;
                        previous[i] = minNode;
                    }
                }
            }
        }

        if (distance[destinationNode] == int.MaxValue)
        {
            return null;
        }

        var path = new LinkedList<int>();
        int? currentNode = destinationNode;
        while (currentNode != null)
        {
            path.AddFirst(currentNode.Value);
            currentNode = previous[currentNode.Value];
        }

        return path.ToList();
    }

    public static void Main()
    {
       
        // Hálózati elemek
        halozatielemek[] Htomb = new halozatielemek[10];
        Htomb[0] = new halozatielemek(0, "switch");
        Htomb[1] = new halozatielemek(1, "switch p1");
        Htomb[2] = new halozatielemek(2, "switch P2");
        Htomb[3] = new halozatielemek(3, "switch P3");
        Htomb[4] = new halozatielemek(4, "switch P4");
        Htomb[5] = new halozatielemek(5, "2/1 faliport");
        Htomb[6] = new halozatielemek(6, "iroda");
        Htomb[7] = new halozatielemek(7, "tárgyaló");
        Htomb[8] = new halozatielemek(8, "titkárnő");
        Htomb[9] = new halozatielemek(9, "2/1 fali port");

        // Összerendelések
        vonal[] Vtomb = new vonal[9];
        Vtomb[0] = new vonal(0, "Eszk.belül", 0, 1);
        Vtomb[1] = new vonal(1, "Eszk.belül", 0, 2);
        Vtomb[2] = new vonal(2, "Eszk.belül", 0, 3);
        Vtomb[3] = new vonal(3, "Eszk.belül", 0, 4);
        Vtomb[4] = new vonal(4, "UTP", 1, 6);
        Vtomb[5] = new vonal(5, "UTP", 3, 7);
        Vtomb[6] = new vonal(7, "UTP", 4, 8);
        Vtomb[7] = new vonal(7, "UTP", 6, 5);
        Vtomb[8] = new vonal(8, "UTP", 7, 9);

        var halozat = new int[Htomb.Length, Htomb.Length];
        // Kapcsolatok feltőltése
        for (int i = 0; i < Vtomb.Length; i++)
        {
            halozat[Vtomb[i].Obj1, Vtomb[i].Obj2] = 1;
            // Ellentétesen is, mindegyiket kétirányúnak véljük, nem súlyozunk, de azt is lehetne
            halozat[Vtomb[i].Obj2, Vtomb[i].Obj1] = 1;
        }
        
        // Igy néz ki a gráfunk
        Console.WriteLine("A gráfunk:");
        for (int i = 0; i < Htomb.Length; i++)
        {
            for (int j = 0; j < Htomb.Length; j++)
            {
                Console.Write(halozat[i, j].ToString() + ' ');
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();

        PrintPath(halozat, 8, 9, Htomb, Vtomb);
        Console.ReadLine();
    }

    public static void PrintPath(int[,] graph, int sourceNode, int destinationNode, halozatielemek[] adatok, vonal[] vonalak)
    {
       Console.WriteLine(
            "Útvonal [{0} -> {1}] között: ",
            sourceNode,
            destinationNode);
        Console.WriteLine();
        var path = Dijkstra.DijkstraAlg(graph, sourceNode, destinationNode);

        if (path == null)
        {
            Console.WriteLine("Nincs út [{0} és {1}] között!");
        }
        else
        {
            int pathLength = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                pathLength += graph[path[i], path[i + 1]];
                Console.Write(adatok[path[i]].nev);
                if (i != path.Count - 2) Console.Write(" -> ");
            }                                                                
            Console.WriteLine();
            Console.WriteLine();

            var formattedPath = string.Join(" -> ", path);
            Console.WriteLine("{0}   ({1} összerendelés)", formattedPath, pathLength);

            
        }
    }

    


}