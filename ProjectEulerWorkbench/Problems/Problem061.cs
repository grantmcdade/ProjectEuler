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
    class Problem061 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of the only set of six 4-digit figurate numbers with a cyclic property."; }
        }

        public string Solve()
        {
            const int start = 999;
            const int end = 10000;

            // Find all the Triangle, Square, Pentagonal, Hexagonal, Heptagonal and Octagonal 4 digit numbers
            var triangleNumbers = new HashSet<int>();
            var squareNumbers = new HashSet<int>();
            var pentagonalNumbers = new HashSet<int>();
            var hexagonalNumbers = new HashSet<int>();
            var heptagonalNumbers = new HashSet<int>();
            var octagonalNumbers = new HashSet<int>();

            bool allNumbersFound = false;
            var n = 0;
            do
            {
                ++n;
                allNumbersFound = true;

                var triangle = (n * (n - 1)) / 2;
                if (triangle > start && triangle < end)
                {
                    triangleNumbers.Add(triangle);
                    allNumbersFound = false;
                }

                var square = n * n;
                if (square > start && square < end)
                {
                    squareNumbers.Add(square);
                    allNumbersFound = false;
                }

                var pentagonal = (n * (3 * n - 1)) / 2;
                if (pentagonal > start && pentagonal < end)
                {
                    pentagonalNumbers.Add(pentagonal);
                    allNumbersFound = false;
                }

                var hexagonal = n * (2 * n - 1);
                if (hexagonal > start && hexagonal < end)
                {
                    hexagonalNumbers.Add(hexagonal);
                    allNumbersFound = false;
                }

                var heptagonal = (n * (5 * n - 3)) / 2;
                if (heptagonal > start && heptagonal < end)
                {
                    heptagonalNumbers.Add(heptagonal);
                    allNumbersFound = false;
                }

                var octagonal = n * (3 * n - 2);
                if (octagonal > start && octagonal < end)
                {
                    octagonalNumbers.Add(octagonal);
                    allNumbersFound = false;
                }

                // When N is a low value the lists will be empty but this does not mean we
                // have found all the numbers, it actually means we have no found any yet
                if (triangleNumbers.Count == 0)
                    allNumbersFound = false;

            } while (!allNumbersFound);

            HashSet<int>[] numberSets = { triangleNumbers, squareNumbers, pentagonalNumbers, hexagonalNumbers, heptagonalNumbers, octagonalNumbers };
            int[] numberSetIndexes = { 0, 1, 2, 3, 4, 5 };

            var sum = 0;
            foreach (var permutation in Sets.GetLexicographicPermutations(numberSetIndexes))
            {
                foreach (var number1 in numberSets[permutation[0]])
                {
                    foreach (var number2 in numberSets[permutation[1]])
                    {
                        // Check if the first two digits of number
                        if ((number1 % 100) == (number2 / 100))
                        {
                            foreach (var number3 in numberSets[permutation[2]])
                            {
                                if ((number2 % 100) == (number3 / 100))
                                {
                                    foreach (var number4 in numberSets[permutation[3]])
                                    {
                                        if ((number3 % 100) == (number4 / 100))
                                        {
                                            foreach (var number5 in numberSets[permutation[4]])
                                            {
                                                if ((number4 % 100) == (number5 / 100))
                                                {
                                                    foreach (var number6 in numberSets[permutation[5]])
                                                    {
                                                        if ((number5 % 100) == (number6 / 100))
                                                        {
                                                            if ((number6 % 100) == (number1 / 100))
                                                            {
                                                                // Set found!!
                                                                sum = number1 + number2 + number3 + number4 + number5 + number6;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Convert.ToString(sum);
        }
    }
}
