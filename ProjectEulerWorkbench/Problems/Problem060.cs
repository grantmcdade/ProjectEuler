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
    class Problem060 : IProblem
    {
        HashSet<ulong> _primes;
        ulong _largestPrime;

        public string Description
        {
            get { return "Find a set of five primes for which any two primes concatenate to produce another prime."; }
        }

        public string Solve()
        {
            const int limit = 5;
            const ulong primesToTest = 10000;
            var primes = Util.SieveOfAtkinInt64(primesToTest);
            _primes = new HashSet<ulong>(primes);
            _largestPrime = primes[primes.Length - 1];
            var currentSet = new List<ulong>(); // { 3, 7, 109, 673 };
            for (int i = 0; i < primes.Length; i++)
            {
                currentSet.Clear();
                currentSet.Add(primes[i]);

                for (int j = i + 1; j < primes.Length; j++)
                {
                    if (IsSpecialPrime(currentSet, primes[j]))
                    {
                        currentSet.Add(primes[j]);
                        for (int l = j + 1; l < primes.Length; l++)
                        {
                            if (IsSpecialPrime(currentSet, primes[l]))
                            {
                                currentSet.Add(primes[l]);
                                for (int m = l + 1; m < primes.Length; m++)
                                {
                                    if (IsSpecialPrime(currentSet, primes[m]))
                                    {
                                        currentSet.Add(primes[m]);
                                        for (int n = m + 1; n < primes.Length; n++)
                                        {
                                            if (IsSpecialPrime(currentSet, primes[n]))
                                            {
                                                // Found first set, and most likely lowest too
                                                currentSet.Add(primes[n]);
                                                break;
                                            }
                                        }
                                        if (currentSet.Count >= limit)
                                            break;
                                        currentSet.Remove(primes[m]);
                                    }
                                }
                                if (currentSet.Count >= limit)
                                    break;
                                currentSet.Remove(primes[l]);
                            }
                        }
                        if (currentSet.Count >= limit)
                            break;
                        currentSet.Remove(primes[j]);
                    }
                }
                if (currentSet.Count >= limit)
                    break;
            }

            if (currentSet.Count < limit)
                return "No solution! :(";

            ulong sum = Util.Sum(currentSet.ToArray());
            return Convert.ToString(sum);
        }

        private bool IsSpecialPrime(List<ulong> currentSet, ulong primeToTest)
        {
            for (int j = 0; j < currentSet.Count; j++)
            {
                var prime1 = Convert.ToString(primeToTest);
                var prime2 = Convert.ToString(currentSet[j]);

                var testValue = UInt64.Parse(prime1 + prime2);
                if (testValue <= _largestPrime)
                {
                    if (!_primes.Contains(testValue))
                        return false;
                }
                else
                {
                    if (!Util.IsPrime(testValue))
                        return false;
                }

                testValue = UInt64.Parse(prime2 + prime1);
                if (testValue <= _largestPrime)
                {
                    if (!_primes.Contains(testValue))
                        return false;
                }
                else
                {
                    if (!Util.IsPrime(testValue))
                        return false;
                }
            }
            return true;
        }
    }
}
