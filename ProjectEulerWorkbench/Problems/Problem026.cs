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
    class Problem026 : IProblem
    {
        public string Description
        {
            get { return "Find the value of d < 1000 for which 1/d contains the longest recurring cycle."; }
        }

        public string Solve()
        {
            const int length = 1000;
            int valueWithLongestCycle = 0;
            int longestCycleLength = 0;
            for (int i = 2; i < length; i++)
            {
                var fraction = GetFraction(i);
                if (fraction.Count > longestCycleLength)
                {
                    valueWithLongestCycle = i;
                    longestCycleLength = fraction.Count;
                }
            }

            return Convert.ToString(valueWithLongestCycle);
        }

        private List<int> GetFraction(int denominator)
        {
            int numerator = 1;
            bool foundCycle = false;
            var fraction = new List<int>();
            var remainders = new Dictionary<int, int>();
            
            // Find the multiple of 10 which can be evenly divided by n
            // thre result of this division is our fractional part
            while (true)
            {
                numerator *= 10;
                int r = numerator % denominator;
                int q = (numerator - r) / denominator;
                if (r == 0)
                {
                    fraction.Add(q);
                    break;
                }
                
                if (remainders.ContainsKey(r))
                {
                    foundCycle = false;

                    foreach (var item in remainders)
                    {
                        if (item.Value == r && q == fraction[item.Key])
                        {
                            foundCycle = true;
                            break;
                        }
                    }

                    if (foundCycle)
                        break;
                }

                remainders[fraction.Count] = r;
                fraction.Add(q);
                numerator = r;
            }

            return fraction;
        }
    }
}
