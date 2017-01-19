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
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem018 : IProblem
    {
        private IPathProvider _provider;

        public Problem018(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "Find the maximum sum travelling from the top of the triangle to the base."; }
        }

        private int[][] ReadNumbers()
        {
            IEnumerable<string> lineReader = File.ReadLines(_provider.GetFullyQualifiedPath("triangle.txt"));
            List<string> lines = new List<string>();
            foreach (var line in lineReader)
            {
                lines.Add(line);
            }

            int[][] numbers = new int[lines.Count()][];
            int i = 0;
            foreach (var line in lines)
            {
                var values = line.Split(' ');
                numbers[i] = new int[values.Count()];
                int j = 0;
                foreach (var value in values)
                {
                    numbers[i][j] = Int32.Parse(value);
                    ++j;
                }
                ++i;
            }
            return numbers;
        }

        public string Solve()
        {
            int[][] numbers =
            {
                new int[] {75},
                new int[] {95, 64},
                new int[] {17, 47, 82},
                new int[] {18, 35, 87, 10},
                new int[] {20, 04, 82, 47, 65},
                new int[] {19 ,01 ,23 ,75 ,03 ,34},
                new int[] {88 ,02 ,77 ,73 ,07 ,63 ,67},
                new int[] {99, 65, 04, 28, 06, 16, 70, 92},
                new int[] {41, 41, 26, 56, 83, 40, 80, 70, 33},
                new int[] {41, 48, 72, 33, 47, 32, 37, 16, 94, 29},
                new int[] {53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14},
                new int[] {70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57},
                new int[] {91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48},
                new int[] {63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31},
                new int[] {04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23}
            };

            // int[][] numbers = ReadNumbers();

            Triangle[][] triangles = new Triangle[numbers.Length][];
            for (int i = 0; i < numbers.Length; i++)
            {
                triangles[i] = new Triangle[numbers[i].Length];
            }

            // Create the triangles
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers[i].Length; j++)
                {
                    var t = new Triangle()
                    {
                        Value = numbers[i][j],
                        X = i,
                        Y = j
                    };
                    triangles[i][j] = t;

                    // Connect this triangle to it's adjacent parents
                    if (i > 0)
                    {
                        if (j < numbers[i - 1].Length)
                            triangles[i - 1][j].Edges.Add(t);
                        if (j > 0)
                            triangles[i - 1][j - 1].Edges.Add(t);
                    }
                }
            }

            VertexStatus[][] status = new VertexStatus[numbers.Length][];
            for (int i = 0; i < numbers.Length; i++)
            {
                status[i] = new VertexStatus[numbers[i].Length];
            }
            ulong[][] totals = new ulong[numbers.Length][];
            for (int i = 0; i < numbers.Length; i++)
            {
                totals[i] = new ulong[numbers[i].Length];
            }
            DepthFirstSearch(triangles[0][0], status, totals);

            return Convert.ToString(totals[0][0]);
        }

        private void DepthFirstSearch(Triangle t, VertexStatus[][] status, ulong[][] totals)
        {
            if (status[t.X][t.Y] == VertexStatus.Undiscovered)
            {
                status[t.X][t.Y] = VertexStatus.Discovered;
                if (t.Edges.Count > 0)
                {
                    DepthFirstSearch(t.Edges[0], status, totals);
                    DepthFirstSearch(t.Edges[1], status, totals);
                    // Always add the value from the child with the largest current total
                    if (totals[t.Edges[0].X][t.Edges[0].Y] > totals[t.Edges[1].X][t.Edges[1].Y])
                        totals[t.X][t.Y] = (ulong)t.Value + totals[t.Edges[0].X][t.Edges[0].Y];
                    else
                        totals[t.X][t.Y] = (ulong)t.Value + totals[t.Edges[1].X][t.Edges[1].Y];
                }
                else
                {
                    totals[t.X][t.Y] = (ulong)t.Value;
                }
                status[t.X][t.Y] = VertexStatus.FullyExplored;
            }
        }

        private enum VertexStatus
        {
            Undiscovered,
            Discovered,
            FullyExplored
        }

        private class Triangle
        {
            private List<Triangle> _edges = new List<Triangle>();

            public int Value { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public List<Triangle> Edges { get { return _edges; } }
        }
    }
}
