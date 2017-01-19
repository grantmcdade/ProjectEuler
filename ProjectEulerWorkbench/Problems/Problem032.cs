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
    class Problem032 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all numbers that can be written as pandigital products."; }
        }

        public string Solve()
        {
            const int length = 512;
            int[] digits = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var pandigitalSets = new List<Tuple<ulong, ulong, ulong>>();
            for (int i = 1; i < length; i++)
            {
                var workingSet = new HashSet<int>(digits);
                int[] numberOneSet = GetDigits(digits, workingSet, i);
                int subsets = (int)Math.Pow(2, workingSet.Count);
                int[] subDigits = workingSet.ToArray();
                for (int j = 1; j < subsets; j++)
                {
                    var subWorkingSet = new HashSet<int>(subDigits);
                    int[] numberTwoSet = GetDigits(subDigits, subWorkingSet, j);

                    // For each lexicographic permutation of numbers one and two
                    // test the product to see if it makes use of all the remaining
                    // digits in the set exactly once each
                    var numberOne = 0UL;
                    Array.Sort(numberOneSet);
                    Array.Sort(numberTwoSet);
                    foreach (var setOne in Sets.GetLexicographicPermutations(numberOneSet))
                    {
                        numberOne = Sets.ToNumber(setOne);
                        foreach (var setTwo in Sets.GetLexicographicPermutations(numberTwoSet))
                        {
                            ulong numberTwo = Sets.ToNumber(setTwo);
                            ulong result = numberOne * numberTwo;
                            int[] resultDigits = Sets.ToDigits(result);
                            var testSet = new HashSet<int>(subWorkingSet);
                            bool invalidDigit = false;
                            for (int k = 0; k < resultDigits.Length; k++)
                            {
                                if (testSet.Contains(resultDigits[k]))
                                {
                                    testSet.Remove(resultDigits[k]);
                                }
                                else
                                {
                                    invalidDigit = true;
                                    break;
                                }
                            }

                            if (!invalidDigit && testSet.Count == 0)
                            {
                                // Found a pandigital product and it's multipliers which uses the full set 1 through 9
                                pandigitalSets.Add(new Tuple<ulong, ulong, ulong>(numberOne, numberTwo, result));
                            }
                        }
                    }
                }
            }

            var products = new HashSet<int>();
            foreach (var item in pandigitalSets)
            {
                products.Add((int)item.Item3);
            }

            return Convert.ToString(products.Sum());
        }

        private int GetNumber(int[] digits, HashSet<int> workingSet, int mask)
        {
            int number = 0;
            for (int j = 0; j < digits.Length; j++)
            {
                if (j >= mask)
                    break;

                int pos = 1 << j;
                if ((mask & pos) == pos)
                {
                    number *= 10;
                    number += digits[j];
                    workingSet.Remove(digits[j]);
                }
            }
            return number;
        }

        private int[] GetDigits(int[] digits, HashSet<int> workingSet, int mask)
        {
            var result = new List<int>();
            for (int j = 0; j < digits.Length; j++)
            {
                if (j >= mask)
                    break;

                int pos = 1 << j;
                if ((mask & pos) == pos)
                {
                    result.Add(digits[j]);
                    workingSet.Remove(digits[j]);
                }
            }
            return result.ToArray();
        }

        private bool IsPandigital(int value)
        {
            string characters = Convert.ToString(value);
            for (int i = 0; i < characters.Length; i++)
            {
                for (int j = i + 1; j < characters.Length; j++)
                {
                    if (characters[i] == characters[j])
                        return false;
                }
            }
            return true;
        }
    }
}
