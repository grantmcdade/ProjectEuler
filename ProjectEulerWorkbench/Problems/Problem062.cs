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
    class Problem062 : IProblem
    {
        public string Description
        {
            get { return "Find the smallest cube for which exactly five permutations of its digits are cube."; }
        }

        public string Solve()
        {
            return Solve3();
        }

        private string Solve3()
        {
            const int setSize = 5;
            var n = 0UL;
            var smallestCube = 0UL;
            var cubesByDigitSum = new Dictionary<Tuple<int, int>, List<ulong>>();
            do
            {
                ++n;
                var cube = n * n * n;
                var sumOfDigits = 0;
                var numberOfDigits = 0;

                var value = cube;
                do
                {
                    ++numberOfDigits;
                    sumOfDigits += (int)(value % 10);
                    value /= 10;
                } while (value > 0);

                var cubeKey = new Tuple<int, int>(sumOfDigits, numberOfDigits);

                if (cubesByDigitSum.ContainsKey(cubeKey))
                {
                    if (cubesByDigitSum[cubeKey].Count >= (setSize - 1))
                    {
                        // Found potential set of cubes check the saved cubes against the current
                        // one to see if we can make a set using only digit permutations
                        var cubesToCheck = cubesByDigitSum[cubeKey];
                        int[] digits = Sets.ToDigits(cube);
                        Array.Sort(digits);
                        var matchingCubes = new List<ulong>();
                        matchingCubes.Add(cube);
                        for (int i = 0; i < cubesToCheck.Count; i++)
                        {
                            var cubeToCheck = cubesToCheck[i];
                            int[] digitsToCheck = Sets.ToDigits(cubeToCheck);
                            Array.Sort(digitsToCheck);
                            if (digits.Length == digitsToCheck.Length)
                            {
                                var isMatch = true;
                                for (int k = 0; k < digits.Length; k++)
                                {
                                    if (digits[k] != digitsToCheck[k])
                                    {
                                        isMatch = false;
                                        break;
                                    }
                                }
                                if (isMatch)
                                    matchingCubes.Add(cubeToCheck);
                            }
                        }

                        if (matchingCubes.Count >= setSize)
                        {
                            smallestCube = UInt64.MaxValue;
                            foreach (var matchingCube in matchingCubes)
                            {
                                if (matchingCube < smallestCube)
                                    smallestCube = matchingCube;
                            }
                        }
                    }
                    cubesByDigitSum[cubeKey].Add(cube);
                }
                else
                {
                    cubesByDigitSum.Add(cubeKey, new List<ulong>());
                    cubesByDigitSum[cubeKey].Add(cube);
                }

            } while (smallestCube == 0UL);

            return Convert.ToString(smallestCube);
        }

        private string Solve1()
        {
            // Find all the cubes up to 500
            var cubes = new HashSet<ulong>();
            for (ulong i = 1; i <= 2000; i++)
                cubes.Add(i * i * i);

            var smallestCube = 0UL;
            foreach (var cube in cubes)
            {
                var digits = Sets.ToDigits(cube);
                Array.Sort(digits);
                int cubePermutations = 0;
                foreach (var permutation in Sets.GetLexicographicPermutations(digits))
                {
                    // Ignore permutations with leading zeros
                    if (permutation[0] == 0)
                        continue;

                    var testValue = Sets.ToNumber(digits);
                    if (cubes.Contains(testValue))
                        ++cubePermutations;
                }
                if (cubePermutations >= 5)
                {
                    smallestCube = cube;
                    break;
                }
            }

            return Convert.ToString(smallestCube);
        }

        private string Solve2()
        {
            var oneThrid = 1d / 3d;
            var smallestCube = 0UL;
            var cube = 0UL;
            var i = 0UL;
            do
            {
                ++i;
                cube = i * i * i;
                var digits = Sets.ToDigits(cube);
                Array.Sort(digits);
                int cubePermutations = 0;
                foreach (var permutation in Sets.GetLexicographicPermutations(digits))
                {
                    // Ignore permutations with leading zeros
                    if (permutation[0] == 0)
                        continue;

                    var testValue = Sets.ToNumber(digits);
                    // This is wrong, we need to test if the number is a cube here
                    // but I don't have an algorithm for that
                    var isCube = Math.Pow(testValue, oneThrid) % 1d;
                    if (isCube > -0.0000001d && isCube < 0.0000001d)
                        ++cubePermutations;
                }
                if (cubePermutations >= 5)
                {
                    smallestCube = cube;
                    break;
                }
            } while (smallestCube == 0);

            return Convert.ToString(smallestCube);
        }

        //private class Key
        //{
        //    public ulong Cube { get; set; }
        //    public int DigitCount { get; set; }

        //    public Key(ulong cube, int digitCount)
        //    {
        //        Cube = cube;
        //        DigitCount = digitCount;
        //    }

        //    public class EqualityComparer : IEqualityComparer<Key>
        //    {
        //        public bool Equals(Key x, Key y)
        //        {
        //            return x.DigitCount == y.DigitCount && x.Cube == y.Cube;
        //        }

        //        public int GetHashCode(Key obj)
        //        {
        //            return (obj.DigitCount * 71) ^ (obj.Cube.GetHashCode() * 71);
        //        }
        //    }
        //}
    }
}
