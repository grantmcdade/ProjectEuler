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
    class Problem037 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all eleven primes that are both truncatable from left to right and right to left."; }
        }

        public string Solve()
        {
            // Find the 11 primes which are truncatable in both directions
            List<ulong> rightToLeftPrimes = new List<ulong>();
            rightToLeftPrimes.Add(2);
            rightToLeftPrimes.Add(3);
            rightToLeftPrimes.Add(5);
            rightToLeftPrimes.Add(7);
            CalculatePrimes(rightToLeftPrimes, 1000, false);

            rightToLeftPrimes.RemoveAt(0);
            rightToLeftPrimes.RemoveAt(0);
            rightToLeftPrimes.RemoveAt(0);
            rightToLeftPrimes.RemoveAt(0);

            ulong sum = 0;
            ulong count = 0;
            var specialPrimes = new HashSet<ulong>();
            for (int i = 0; i < rightToLeftPrimes.Count; i++)
            {
                if(IsLeftToRightPrime(rightToLeftPrimes[i]))
                {
                    ++count;
                    sum += rightToLeftPrimes[i];
                    specialPrimes.Add(rightToLeftPrimes[i]);
                }
            }

            return Convert.ToString(sum);
        }

        private bool IsLeftToRightPrime(ulong value)
        {
            // Remove digits from the front of the number
            // and test if each step produces a prime
            ulong filter = 1;
            ulong testValue = value;
            do
            {
                filter = 1;
                do
                {
                    filter *= 10;
                } while (filter < testValue);
                filter /= 10;

                if (filter == 1)
                    break;

                testValue = value % filter;
                if (!Util.IsPrime(testValue))
                {
                    return false;
                }
            } while (true);

            return true;
        }

        private void CalculatePrimes(List<ulong> primes, int limit, bool addBefore)
        {
            HashSet<ulong> primeSet = new HashSet<ulong>(primes);
            int digitPosition = 0;
            int length = 0;
            do
            {
                ++digitPosition;
                
                ulong shift = 0;
                if (addBefore) // We only need the shift variable when adding digits to the top
                    shift = Util.Pow(10ul, digitPosition);

                int start = length;
                length = primes.Count;
                for (int i = start; i < length; i++)
                {
                    for (ulong digit = 0; digit < 10; digit++)
                    {
                        ulong testValue = primes[i];
                        if (addBefore)
                            testValue += digit * shift;
                        else
                            testValue = (testValue * 10) + digit;

                        if (Util.IsPrime(testValue))
                        {
                            if (!primeSet.Contains(testValue))
                            {
                                primeSet.Add(testValue);
                                primes.Add(testValue);
                            }
                        }
                    }
                }

                // If we did not find any more primes then exit the loop
                if (primes.Count == length)
                    break;

            } while (primes.Count < limit);
        }
    }
}
