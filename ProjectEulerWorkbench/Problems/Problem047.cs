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
    class Problem047 : IProblem
    {
        public string Description
        {
            get { return "Find the first four consecutive integers to have four distinct primes factors."; }
        }

        public string Solve()
        {
            ulong i = 0;
            int consecutive = 0;
            int factorsRequired = 4;
            do
            {
                ++i;
                ulong[] factors = Util.GetFactors(i);
                if (factors.Length >= factorsRequired)
                {
                    int primeCount = 0;
                    for (int j = 0; j < factors.Length; j++)
                    {
                        if (Util.IsPrime(factors[j]))
                        {
                            ++primeCount;
                        }
                    }
                    if (primeCount >= factorsRequired)
                    {
                        ++consecutive;
                    }
                    else
                    {
                        consecutive = 0;
                    }
                }
                else
                {
                    consecutive = 0;
                }
                if (consecutive >= factorsRequired)
                    return Convert.ToString(i - ((ulong)consecutive - 1));
            } while (true);
        }
    }
}
