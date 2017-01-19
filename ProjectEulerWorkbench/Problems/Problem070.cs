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
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem070 : IProblem
    {
        public string Description
        {
            get { return "Investigate values of n for which φ(n) is a permutation of n."; }
        }

        public string Solve()
        {
            const int limit = 10000000;
            var sieve = new PrimeSieveInt32() { IncludeTotients = true };
            sieve.Initialise(limit);
            var minN = Double.MaxValue;
            var n = 0;
            for (int i = 2; i < limit; i++)
            {
                var phi = sieve.Phi(i);

                int[] digits = Sets.ToDigits((ulong)i);
                int[] phiDigits = Sets.ToDigits((ulong)phi);
                if (digits.Length == phiDigits.Length)
                {
                    Array.Sort(digits);
                    Array.Sort(phiDigits);
                    var isMatch = true;
                    for (int j = 0; j < digits.Length; j++)
                    {
                        if (digits[j] != phiDigits[j])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        var ratio = (double)i / (double)phi;
                        if (ratio < minN)
                        {
                            minN = ratio;
                            n = i;
                        }
                    }
                }
            }

            return Convert.ToString(n);
        }

        //public string Solve()
        //{
        //    const int limit = 10000000;

        //    _primes = Util.SieveOfAtkinInt32(limit);
        //    //int[] totients = new int[limit];

        //    var minN = 0;
        //    var minRatio = double.MaxValue;

        //    //var lastN = 0;
        //    //var lastPhi = 0;

        //    for (int n = 2; n < limit; n++)
        //    {
        //        var phi = 0;
        //        //if (totients[n] == 0)
        //        //{
        //        phi = (int)Phi(n);
        //        //}
        //        //else
        //        //{
        //        //    // Use the cached totiens when available
        //        //    phi = totients[n];
        //        //}

        //        // Cache the totiens we calculate
        //        //if (lastN != 0)
        //        //{
        //        //    var index = (ulong)n * (ulong)lastN;
        //        //    if (index < (ulong)totients.Length)
        //        //        totients[index] = phi * lastPhi;
        //        //}
        //        //lastN = n;
        //        //lastPhi = phi;

        //        var nStr = n.ToString();
        //        var phiStr = phi.ToString();
        //        if (nStr.Length == phiStr.Length)
        //        {
        //            var isMatch = true;
        //            var nChars = nStr.ToCharArray();
        //            var phiChars = phiStr.ToCharArray();
        //            Array.Sort(nChars);
        //            Array.Sort(phiChars);
        //            for (int i = 0; i < nChars.Length; i++)
        //            {
        //                if (nChars[i] != phiChars[i])
        //                {
        //                    isMatch = false;
        //                    break;
        //                }
        //            }
        //            if (isMatch)
        //            {
        //                // Special totient found!
        //                var ratio = (double)n / (double)phi;
        //                if (ratio < minRatio)
        //                {
        //                    minRatio = ratio;
        //                    minN = n;
        //                }
        //            }
        //        }
        //    }

        //    return Convert.ToString(minN);
        //}

        //int[] _primes;
        //private long Phi(long n)
        //{
        //    return Util.Phi((int)n, _primes);
        //}
    }
}
