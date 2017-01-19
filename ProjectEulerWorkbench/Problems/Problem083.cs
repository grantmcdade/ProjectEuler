/*
    Grant McDade
    Copyright(C) 2017 Grant McDade

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem083 : IProblem
    {
        private IPathProvider _provider;

        public Problem083(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "Find the minimal path sum from the top left to the bottom right by moving left, right, up, and down."; }
        }

        const int limit = 79;

        public string Solve()
        {
            var lines = File.ReadAllLines(_provider.GetFullyQualifiedPath("matrix.txt"));
            Debug.Assert(lines.Length == 80);
            var matrix = new int[80, 80];
            for (int i = 0; i < lines.Length; i++)
            {
                var numbers = lines[i].Split(',');
                Debug.Assert(numbers.Length == 80);
                for (int j = 0; j < numbers.Length; j++)
                {
                    matrix[i, j] = Int32.Parse(numbers[j]);
                }
            }

            //var matrix = new int[,] { {131, 673, 234, 103,  18 },
            //                          {201,  96, 342, 965, 150 },
            //                          {630, 803, 746, 422, 111},
            //                          {537, 699, 497, 121, 956},
            //                          {805, 732, 524,  37, 331} };

            Debug.Assert(limit == matrix.GetLength(0) - 1);

            var nodes = new Node[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    nodes[i, j] = new Node(i * matrix.GetLength(0) + j, matrix[i, j], j, i);
                    // Add the edges on the previous nodes
                    if (i != 0)
                    {
                        nodes[i - 1, j].Edges.Add(nodes[i, j]);
                        nodes[i, j].Edges.Add(nodes[i - 1, j]);
                    }
                    if (j != 0)
                    {
                        nodes[i, j - 1].Edges.Add(nodes[i, j]);
                        nodes[i, j].Edges.Add(nodes[i, j - 1]);
                    }
                }
            }
            nodes[limit, limit].Edges.Clear();

            var shortestCost = 0;
            var stopwatch = Stopwatch.StartNew();
            var shortestPath = AStar(nodes[0, 0], nodes[limit, limit]);
            var astarTime = stopwatch.ElapsedMilliseconds;
            if (shortestPath != null)
            {
                foreach (var node in shortestPath)
                {
                    shortestCost += node.Value;
                }
            }


            return Convert.ToString(shortestCost) + " - " + astarTime.ToString() + "ms";
        }

        /// <summary>
        /// An implementation of the A* path finding algorithm
        /// </summary>
        /// <param name="start">The start node of the graph to search</param>
        /// <param name="goal">The destination node to find</param>
        /// <returns></returns>
        private List<Node> AStar(Node start, Node goal)
        {
            var closedSet = new HashSet<Node>();
            var openSet = new HashSet<Node>() { start };
            var openQueue = new Heap<Node>(new NodeFComparer());
            var cameFrom = new Dictionary<Node, Node>();

            start.G = 0;
            start.F = HeuristicCostEstimate(start, goal);
            openQueue.Insert(start);

            while (openQueue.Size > 0)
            {
                Node current = openQueue.ExtractMax();

                //Debug.WriteLine("Starting {0}", current);
                if (current.Equals(goal))
                {
                    //Debug.WriteLine("Solution found!");
                    return ReconstructPath(cameFrom, goal);
                }

                openSet.Remove(current);
                closedSet.Add(current);
                foreach (var neighbor in current.Edges)
                {
                    //Debug.WriteLine("Starting neighbor {0}", neighbor);

                    // An edge connects two nodes so we can't be sure which one
                    // is the current node and which one is the other node
                    if (closedSet.Contains(neighbor))
                    {
                        //Debug.WriteLine("Skipping {0} since it is already processed", neighbor);
                        continue;
                    }

                    var tentative_g_score = current.G + current.Value;

                    if (!openSet.Contains(neighbor) || tentative_g_score < neighbor.G)
                    {
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }

                        //if (cameFrom.ContainsKey(neighbor))
                        //    Debug.WriteLine("Changing path of {0} from {1} to {2}", neighbor, cameFrom[neighbor], current);
                        //else
                        //    Debug.WriteLine("Adding path from {0} to {1}", current, neighbor);

                        cameFrom[neighbor] = current;
                        neighbor.G = tentative_g_score;
                        neighbor.F = neighbor.G + HeuristicCostEstimate(neighbor, goal);

                        // Important, only insert the neighbor after the F value has been set! Otherwise
                        // the queue priority will not be correct
                        openQueue.Insert(neighbor);

                        //Debug.WriteLine("Procesed {0}: g={1} and f={2}", neighbor, g_score[neighbor], f_score[neighbor]);
                    }
                }
            }

            // Failed to find a path!
            return null;
        }

        private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node currentNode)
        {
            if (cameFrom.ContainsKey(currentNode))
            {
                var path = ReconstructPath(cameFrom, cameFrom[currentNode]);
                path.Add(currentNode);
                return path;
            }
            else
            {
                return new List<Node>() { currentNode };
            }
        }

        private int HeuristicCostEstimate(Node start, Node goal)
        {
            return ((goal.X - start.X) + (goal.Y - start.Y)) * 18;
        }

        private class Node : IEquatable<Node>, IComparable<Node>
        {
            public List<Node> Edges { get; private set; }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Value { get; private set; }
            public int G { get; set; }
            public int F { get; set; }

            public Node(int id, int value, int x, int y)
            {
                Value = value;
                X = x;
                Y = y;
                Edges = new List<Node>();
            }

            public bool Equals(Node other)
            {
                return X == other.X && Y == other.Y;
            }

            public int CompareTo(Node other)
            {
                var compareX = X.CompareTo(other.X);
                if (compareX == 0)
                    return Y.CompareTo(other.Y);
                else
                    return compareX;
            }

            public override bool Equals(object obj)
            {
                return this.Equals((Node)obj);
            }

            public override int GetHashCode()
            {
                return (X.GetHashCode() * 31) ^ (Y.GetHashCode() * 31);
            }

            public override string ToString()
            {
                return String.Format("(x={0}, y={1}) value={2}, g={3}, f={4}", X, Y, Value, G, F);
            }
        }

        private class NodeFComparer : Comparer<Node>
        {
            public override int Compare(Node x, Node y)
            {
                if (x == null)
                {
                    if (y == null)
                        return 0;
                    return 1;
                }

                if (y == null)
                    return -1;

                return y.F.CompareTo(x.F);
            }
        }
    }
}
