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

namespace ProjectEulerWorkbench.Problems
{
    class Problem015 : IProblem
    {
        public string Description
        {
            get { return "Starting in the top left corner in a 20 by 20 grid, how many routes are there to the bottom right corner?"; }
        }

        public string Solve()
        {
            const int size = 21;
            // Create the graph of points along with the edge lists
            Point[,] points = new Point[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Create the point at this location
                    var pt = new Point(i, j);
                    points[i, j] = pt;

                    // Connect it to it's parents
                    if (j > 0) points[i, j - 1].Edges.Add(pt);
                    if (i > 0) points[i - 1, j].Edges.Add(pt);
                }
            }

            VertexState[,] state = new VertexState[size, size];
            ulong[,] paths = new ulong[size, size];
            DepthFirstSearch(points[0, 0], state, paths);

            return Convert.ToString(paths[0,0]);
        }

        private void DepthFirstSearch(Point pt, VertexState[,] state, ulong[,] paths)
        {
            if (state[pt.X, pt.Y] == VertexState.Undiscovered)
            {
                state[pt.X, pt.Y] = VertexState.Discovered;
                // Once we reach the leaf node we set the path to 1 so initiate the counting
                if (pt.Edges.Count == 0)
                    paths[pt.X, pt.Y] = 1;
                foreach (var edge in pt.Edges)
                {
                    DepthFirstSearch(edge, state, paths);
                    // The number of paths from this node to the leaf
                    // is the sum of the number of paths from it's children
                    // to the leaf
                    paths[pt.X, pt.Y] += paths[edge.X, edge.Y];
                }
                state[pt.X, pt.Y] = VertexState.FullyExplored;
            }
        }

        private enum VertexState
        {
            Undiscovered,
            Discovered,
            FullyExplored
        }

        private class Point
        {
            private List<Point> _edges;

            public int X { get; set; }
            public int Y { get; set; }
            public List<Point> Edges
            {
                get { return _edges; }
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
                _edges = new List<Point>();
            }
        }
    }
}
