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
    class Problem049 : IProblem
    {
        public string Description
        {
            get { return "Find arithmetic sequences, made of prime terms, whose four digits are permutations of each other."; }
        }

        public string Solve()
        {
            var primesSequence = new List<ulong>();
            var primes = new HashSet<ulong>(Util.SieveOfAtkinInt64(10000));
            foreach (var prime in primes)
            {
                if (prime > 999)
                {
                    int[] digits = Sets.ToDigits(prime);
                    Array.Sort(digits);
                    primesSequence.Clear();

                    foreach (var set in Sets.GetLexicographicPermutations(digits))
                    {
                        ulong value = Sets.ToNumber(set);
                        if (value > 999 && primes.Contains(value))
                        {
                            primesSequence.Add(value);
                        }
                    }
                    if (primesSequence.Count >= 3)
                    {
                        // Found possible solution!
                        bool isSequence = false;
                        Tuple<ulong, ulong, ulong> sequence = null;
                        for (int i = 0; i < primesSequence.Count - 1; i++)
                        {
                            for (int j = i + 1; j < primesSequence.Count; j++)
                            {
                                ulong diff = primesSequence[j] - primesSequence[i];
                                ulong nextValue = primesSequence[j] + diff;

                                if (nextValue > primesSequence[primesSequence.Count - 1])
                                {
                                    break;
                                }

                                if (primesSequence.Contains(nextValue))
                                {
                                    isSequence = true;
                                    sequence = new Tuple<ulong, ulong, ulong>(primesSequence[i], primesSequence[j], nextValue);
                                    break;
                                }
                            }
                        }
                        if (isSequence && sequence.Item1 != 1487)
                        {
                            return Convert.ToString(sequence.Item1) + Convert.ToString(sequence.Item2) + Convert.ToString(sequence.Item3);
                        }
                    }
                }
            }
            return String.Empty;
        }
    }
}
