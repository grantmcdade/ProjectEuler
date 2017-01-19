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
using System.Diagnostics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem075 : IProblem
    {
        public string Description
        {
            get { return "Find the number of different lengths of wire that can form a right angle triangle in only one way."; }
        }

        /// <summary>
        /// The transforms find all primitive right triangles (gcd == 1), using those as a starting
        /// point we run through all permutations less than 1000 by multiplying with a variable k
        /// </summary>
        /// <returns></returns>
        public string Solve()
        {
            const int limit = 1500000;
            var Matrices = new int[3][];
            Matrices[0] = new int[] { 1, 2, 2, -2, -1, -2, 2, 2, 3 };
            Matrices[1] = new int[] { 1, 2, 2, 2, 1, 2, 2, 2, 3 };
            Matrices[2] = new int[] { -1, -2, -2, 2, 1, 2, 2, 2, 3 };
            var permutations = new int[limit / 2 + 1];
            var itemsToProcess = new Queue<int[]>();
            var start = new int[] { 3, 4, 5 };
            itemsToProcess.Enqueue(start);
            ++permutations[6]; // Count this first instance
            //itemsToProcess.Enqueue(new int[] { 6, 8, 10 });
            //++permutations[12];
            var tripleCount = 2;
            ProcessNonPrimitives(limit, permutations, ref tripleCount, start);
            while (itemsToProcess.Count > 0)
            {
                var item = itemsToProcess.Dequeue();
                for (int i = 0; i < 3; i++)
                {
                    var result = Transform(item, Matrices[i]);
                    // Confirm that this is a right triangle
                    Debug.Assert(result[0] * result[0] + result[1] * result[1] == result[2] * result[2]);
                    var p = result[0] + result[1] + result[2];
                    if (p <= limit)
                    {
                        var index = p / 2;
                        ++permutations[index];
                        itemsToProcess.Enqueue(result);
                        ++tripleCount;

                        ProcessNonPrimitives(limit, permutations, ref tripleCount, result);
                    }
                }
            }

            var count = 0;
            for (int i = 0; i < permutations.Length; i++)
            {
                if (permutations[i] == 1)
                    ++count;
            }

            return Convert.ToString(count);
        }

        private static void ProcessNonPrimitives(int limit, int[] permutations, ref int tripleCount, int[] result)
        {
            var p = 0;
            var k = 2;
            var nonPrimitives = new int[3];
            do
            {
                nonPrimitives[0] = result[0] * k;
                nonPrimitives[1] = result[1] * k;
                nonPrimitives[2] = result[2] * k;
                ++k;
                p = nonPrimitives[0] + nonPrimitives[1] + nonPrimitives[2];
                if (p <= limit)
                {
                    var index = p / 2;
                    ++permutations[index];
                    ++tripleCount;
                }
            } while (p <= limit);
        }

        private int[] Transform(int[] triple, int[] matrix)
        {
            Debug.Assert(triple.Length == 3);
            Debug.Assert(matrix.Length == 9);

            var result = new int[3];

            result[0] = triple[0] * matrix[0] + triple[1] * matrix[3] + triple[2] * matrix[6];
            result[1] = triple[0] * matrix[1] + triple[1] * matrix[4] + triple[2] * matrix[7];
            result[2] = triple[0] * matrix[2] + triple[1] * matrix[5] + triple[2] * matrix[8];

            return result;
        }
    }
}
