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
using System.Numerics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem357 : IProblem
    {
        public string Description
        {
            get { return "Prime generating integers"; }
        }

        const int _limit = 100000000;
        bool[] _primes;

        // This is the second attempt based on the commens from the
        // project Euler forums, it runs in under 8 seconds.
        public string Solve()
        {
            // Prepare a list of primes
            _primes = new bool[_limit + 1];
            _primes[2] = true;
            for (int i = 3; i <= _limit; i += 2)
                _primes[i] = true;
            var sq = (int)Math.Sqrt(_limit);
            // Cancel the squares
            for (var i = 3; i <= sq; i += 2)
                if (_primes[i])
                    for (var j = i * i; j <= _limit; j += i)
                        _primes[j] = false;

            var result = 0L;
            for (int i = 1; i <= _limit; i++)
            {
                if (_primes[i] && Check(i - 1))
                {
                    result += i - 1;
                }
            }

            return Convert.ToString(result);
        }

        private bool Check(int n)
        {
            var sq = (int)Math.Sqrt(n);
            for (var i = 2; i <= sq; ++i)
            {
                if (n % i == 0)
                {
                    var s = i + n / i;
                    if (!_primes[s])
                        return false;
                }
            }
            return true;
        }

        // This was my first attempt, it found the answer but took more than 10 minutes to run, I was
        // being very careful not to have any overruns etc. because it was taking long to run and
        // I did not want to get it wrong after waiting for 10 mintes.
        private string Solve01()
        {
            BigInteger sum = 0;
            HashSet<ulong> primeSet = new HashSet<ulong>(Util.SieveOfAtkinInt64(_limit));
            for (ulong n = 1; n <= _limit; n++)
            {
                // If n + 1 is not prime then this is not an all factors value
                if (!primeSet.Contains(n + 1))
                    continue;

                ulong[] factors = Util.GetFactors(n);
                bool allFactorsPrime = true;
                for (int i = 0; i < factors.Length; i++)
                {
                    ulong d = factors[i];
                    ulong testValue = d + n / d;
                    if (!primeSet.Contains(testValue))
                    {
                        allFactorsPrime = false;
                        break;
                    }
                }
                if (allFactorsPrime)
                    sum += n;
            }
            return Convert.ToString(sum);
        }
    }
}
