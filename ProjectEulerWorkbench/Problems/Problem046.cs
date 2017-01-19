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
    class Problem046 : IProblem
    {
        public string Description
        {
            get { return "What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?"; }
        }

        public string Solve()
        {
            ulong prime = 0;
            ulong composite = 1;
            var primes = new List<ulong>();
            primes.Add(2);
            do
            {
                ++composite;
                if (composite % 2 != 0)
                {
                    if (Util.IsPrime(composite))
                    {
                        prime = composite;
                        primes.Add(prime);
                        continue;
                    }

                    bool solutionExists = false;
                    for (int i = 0; i < primes.Count; i++)
                    {
                        ulong result;
                        ulong square = 0;
                        do
                        {
                            ++square;
                            result = primes[i] + 2 * square * square;
                        } while (result < composite);
                        if (result == composite)
                        {
                            solutionExists = true;
                            break;
                        }
                    }

                    if (!solutionExists)
                    {
                        // Found the composite which breaks the rule
                        return Convert.ToString(composite);
                    }
                }
            } while (true);
        }
    }
}
