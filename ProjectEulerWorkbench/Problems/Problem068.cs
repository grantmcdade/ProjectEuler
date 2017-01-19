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
using System.Collections;
using System.Diagnostics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem068 : IProblem
    {
        public string Description
        {
            get { return "What is the maximum 16-digit string for a \"magic\" 5-gon ring?"; }
        }

        public string Solve()
        {
            var digits = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var innerRing = new int[5];
            var outerRing = new int[5];
            var maxGonValue = 0UL;

            foreach (var permutation in Sets.GetLexicographicPermutations(digits))
            {
                innerRing[0] = permutation[0];
                innerRing[1] = permutation[1];
                innerRing[2] = permutation[2];
                innerRing[3] = permutation[3];
                innerRing[4] = permutation[4];

                outerRing[0] = permutation[5];
                outerRing[1] = permutation[6];
                outerRing[2] = permutation[7];
                outerRing[3] = permutation[8];
                outerRing[4] = permutation[9];

                var sum1 = innerRing[0] + innerRing[1] + outerRing[0];
                var sum2 = innerRing[1] + innerRing[2] + outerRing[1];
                var sum3 = innerRing[2] + innerRing[3] + outerRing[2];
                var sum4 = innerRing[3] + innerRing[4] + outerRing[3];
                var sum5 = innerRing[4] + innerRing[0] + outerRing[4];
                if (sum1 == sum2 && sum1 == sum3 && sum1 == sum4 && sum1 == sum5)
                {
                    var gonValue = PrintGon(innerRing, outerRing, sum1);
                    if (gonValue > maxGonValue && gonValue < 10000000000000000)
                    {
                        maxGonValue = gonValue;
                    }
                }
            }

            return Convert.ToString(maxGonValue);
        }

        private ulong PrintGon(int[] innerRing, int[] outerRing, int sum)
        {
            // Print the rings clockwise starting from the lowest outer ring value
            var start = 0;
            var gonValue = new StringBuilder();
            var minOuter = Int32.MaxValue;
            for (int i = 0; i < outerRing.Length; i++)
            {
                if (outerRing[i] < minOuter)
                {
                    start = i;
                    minOuter = outerRing[i];
                }
            }

            var counter = 0;
            while (counter < outerRing.Length)
            {
                ++counter;
                //Debug.Write(outerRing[start]);
                gonValue.Append(outerRing[start]);
                switch (start)
                {
                    case 0:
                        //Debug.Write("," + innerRing[0] + "," + innerRing[1] + ";");
                        gonValue.Append(innerRing[0]);
                        gonValue.Append(innerRing[1]);
                        break;
                    case 1:
                        //Debug.Write("," + innerRing[1] + "," + innerRing[2] + ";");
                        gonValue.Append(innerRing[1]);
                        gonValue.Append(innerRing[2]);
                        break;
                    case 2:
                        //Debug.Write("," + innerRing[2] + "," + innerRing[3] + ";");
                        gonValue.Append(innerRing[2]);
                        gonValue.Append(innerRing[3]);
                        break;
                    case 3:
                        //Debug.Write("," + innerRing[3] + "," + innerRing[4] + ";");
                        gonValue.Append(innerRing[3]);
                        gonValue.Append(innerRing[4]);
                        break;
                    case 4:
                        //Debug.Write("," + innerRing[4] + "," + innerRing[0] + ";");
                        gonValue.Append(innerRing[4]);
                        gonValue.Append(innerRing[0]);
                        break;
                    default:
                        break;
                }

                ++start;
                if (start >= outerRing.Length)
                    start = 0;
            }
            //Debug.Write(" Sum = " + sum);
            //Debug.WriteLine(String.Empty);

            return UInt64.Parse(gonValue.ToString());
        }

        public string Solve_Test()
        {
            const int ringSize = 3;
            var digits = new int[] { 1, 2, 3, 4, 5, 6 };
            var sets = new HashSet<int[]>();
            var setsBySum = new Dictionary<int, List<int[]>>();

            // Obtain all the possible 3 digits sets which can be created
            // and group the sets by their sum
            foreach (var set in Sets.GetBinaryPermutations(digits, 3))
            {
                sets.Add(set);

                var sum = set.Sum();
                if (!setsBySum.ContainsKey(sum))
                    setsBySum.Add(sum, new List<int[]>());
                setsBySum[sum].Add(set);
            }

            foreach (var item in setsBySum)
            {
                // For all the groups which have three or more sets
                // find the arrangements which satisfy the rules of
                // the "magic" gon ring.
                if (item.Value.Count >= 3)
                {
                    var ring = new int[ringSize, 3];
                    // Find the common digits in all the sets
                    var digitCoverage = new int[digits.Length];
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        var set = item.Value[i];
                        ++digitCoverage[set[0] - 1];
                        ++digitCoverage[set[1] - 1];
                        ++digitCoverage[set[2] - 1];
                    }

                    // For each digit with a set coverage of 2 or more we
                    // should be able to build a "magic" gon ring
                    var workingGroup = new List<int[]>(item.Value);
                    var resultGroup = new List<int[]>();
                    for (int i = 0; i < digitCoverage.Length; i++)
                    {
                        // Select the first digit
                        if (digitCoverage[i] >= 2)
                        {
                            // Select the second digit
                            for (int j = i + 1; j < digitCoverage.Length; j++)
                            {
                                if (digitCoverage[j] >= 2)
                                {
                                    var digit1 = digitCoverage[i];
                                    var digit2 = digitCoverage[j];

                                    // Select a set containing both digits 1 and 2
                                    for (int k = 0; k < workingGroup.Count; k++)
                                    {
                                        if ((workingGroup[k][0] == digit1 || workingGroup[k][1] == digit1 || workingGroup[k][2] == digit1) &&
                                            (workingGroup[k][0] == digit2 || workingGroup[k][1] == digit2 || workingGroup[k][2] == digit2))
                                        {
                                            resultGroup.Add(workingGroup[k]);
                                            workingGroup.RemoveAt(k);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            return String.Empty;
        }
    }
}
