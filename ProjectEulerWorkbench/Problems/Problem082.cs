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
    class Problem082 : IProblem
    {
        private IPathProvider _provider;

        public Problem082(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "Find the minimal path sum from the left column to the right column."; }
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
                    nodes[i, j] = new Node(i * matrix.GetLength(0) + j, matrix[i, j]);
                    if (i != 0 && j != matrix.GetLength(1) - 1)
                    {
                        nodes[i, j].Edges.Add(nodes[i - 1, j]);
                        nodes[i - 1, j].Edges.Add(nodes[i, j]);
                    }

                    if (j != 0)
                        nodes[i, j - 1].Edges.Add(nodes[i, j]);

                }
            }

            var shortestPath = Int32.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var result = DFS(nodes[i, 0], null);
                if (result < shortestPath)
                    shortestPath = result;
            }
            return Convert.ToString(shortestPath);

            //var shortestPath = Int32.MaxValue;
            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    var cache = new int[matrix.GetLength(0), matrix.GetLength(1)];
            //    var visited = new bool[matrix.GetLength(0) * 2, matrix.GetLength(1) * 2];
            //    var result = ShortestPath(i, 0, matrix, cache, visited);
            //    if (result < shortestPath)
            //        shortestPath = result;
            //}
            //return Convert.ToString(shortestPath);
        }

        #region Depth First Search without a graph
        private int ShortestPath(int i, int j, int[,] matrix, int[,] cache, bool[,] visited)
        {
            if (j == limit)
                return matrix[i, j];

            //if (cache[i, j] != 0)
            //    return cache[i, j];

            var path1 = Int32.MaxValue;
            if (i < limit && !IsVisited(i, j, i + 1, j, visited))
            {
                SetVisited(i, j, i + 1, j, visited);
                path1 = ShortestPath(i + 1, j, matrix, cache, visited);
            }

            var path2 = Int32.MaxValue;
            if (j < limit && !IsVisited(i, j, i, j + 1, visited))
            {
                SetVisited(i, j, i, j + 1, visited);
                path2 = ShortestPath(i, j + 1, matrix, cache, visited);
            }

            var path3 = Int32.MaxValue;
            if (i > 0 && !IsVisited(i, j, i - 1, j, visited))
            {
                SetVisited(i, j, i - 1, j, visited);
                path3 = ShortestPath(i - 1, j, matrix, cache, visited);
            }

            var shortestPath = Math.Min(path1, Math.Min(path2, path3));
            //cache[i, j] = matrix[i, j] + shortestPath;
            //return cache[i, j];
            return matrix[i, j] + shortestPath;
        }

        private bool IsVisited(int i, int j, int i1, int j1, bool[,] visited)
        {
            return visited[(i * 2) + (i1 - i), (j * 2) + (j1 - j)];
        }

        private void SetVisited(int i, int j, int i1, int j1, bool[,] visited)
        {
            visited[(i * 2) + (i1 - i), (j * 2) + (j1 - j)] = true;
        }
        #endregion

        private int DFS(Node node, Node previousNode)
        {
            if (previousNode != null && node.ShortestPath.ContainsKey(previousNode.Id))
                return node.ShortestPath[previousNode.Id];

            if (node.Edges.Count == 0)
            {
                //Debug.WriteLine("Located Edge Node: " + node.Value);
                return node.Value;
            }

            var shortestPath = Int32.MaxValue;
            if (node.State == VertexState.Undiscovered)
            {
                //Debug.WriteLine("Visiting Node: " + node.Value);
                // Set the node to discovered to ensure that
                // we don't loop back on it
                node.State = VertexState.Discovered;

                foreach (var child in node.Edges)
                {
                    if (child.State == VertexState.Undiscovered)
                    {
                        var value = DFS(child, node);
                        if (value < shortestPath)
                            shortestPath = value;
                    }
                }

                // Revert back to undiscovered so that this
                // node can be used in subsequent paths
                node.State = VertexState.Undiscovered;
            }
            var result = 0;
            if (shortestPath != Int32.MaxValue)
                result = node.Value + shortestPath;
            else
                result = node.Value;

            if (previousNode != null)
                node.ShortestPath.Add(previousNode.Id, result);
            //Debug.WriteLine("Completed Node: " + node.Value + " with result = " + result);
            return result;
        }

        private enum VertexState
        {
            Undiscovered,
            Discovered,
            FullyExplored
        }

        private class Node
        {
            public List<Node> Edges { get; private set; }
            public Dictionary<int, int> ShortestPath { get; private set; }

            public VertexState State { get; set; }
            public int Id { get; private set; }
            public int Value { get; private set; }

            public Node(int id, int value)
            {
                Id = id;
                Value = value;
                Edges = new List<Node>();
                ShortestPath = new Dictionary<int, int>();
            }
        }
    }
}
