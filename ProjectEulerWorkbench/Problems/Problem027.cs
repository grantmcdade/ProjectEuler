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
    class Problem027 : IProblem
    {
        public string Description
        {
            get { return "Find a quadratic formula that produces the maximum number of primes for consecutive values of n."; }
        }

        public string Solve()
        {
            long longestSequence = 0;
            long productOfCoefficients = 0;

            for (long a = -999; a < 1000; a++)
            {
                for (long b = -999; b < 1000; b++)
                {
                    // Process a, b
                    long n = -1;
                    long prime = 0;
                    do
                    {
                        ++n;
                        prime = Next(n, a, b);
                    } while (Util.IsPrime(prime));
                    if (n > longestSequence)
                    {
                        longestSequence = n;
                        productOfCoefficients = a * b;
                    }

                    // Process b, a
                    n = -1;
                    do
                    {
                        ++n;
                        prime = Next(n, b, a);
                    } while (Util.IsPrime(prime));
                    if (n > longestSequence)
                    {
                        longestSequence = n;
                        productOfCoefficients = a * b;
                    }
                }
            }

            return Convert.ToString(productOfCoefficients);
        }

        private long Next(long n, long a, long b)
        {
            return n * n + a * n + b;
        }
    }
}
