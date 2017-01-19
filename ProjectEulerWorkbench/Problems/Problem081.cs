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
    class Problem081 : IProblem
    {
        private IPathProvider _provider;

        public Problem081(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "Find the minimal path sum from the top left to the bottom right by moving right and down."; }
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
            
            var cache = new int[matrix.GetLength(0), matrix.GetLength(1)];
            var result = ShortestPath(0, 0, matrix, cache);
            return Convert.ToString(result);
        }

        private int ShortestPath(int i, int j, int[,] matrix, int[,] cache)
        {
            if (i == limit & j == limit)
                return matrix[i, j];

            if (cache[i, j] != 0)
                return cache[i, j];

            var path1 = Int32.MaxValue;
            if (i < limit)
                path1 = ShortestPath(i + 1, j, matrix, cache);

            var path2 = Int32.MaxValue;
            if (j < limit)
                path2 = ShortestPath(i, j + 1, matrix, cache);

            cache[i, j] = matrix[i, j] + (path1 < path2 ? path1 : path2);
            return cache[i, j];
        }
    }
}
