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
    class Problem064 : IProblem
    {
        public string Description
        {
            get { return "How many continued fractions for N ≤ 10000 have an odd period?"; }
        }

        public string Solve()
        {
            var oddPeriod = 0;
            var fractions = new HashSet<Tuple<int, int, int>>();
            
            for (int S = 2; S <= 10000; S++)
            {
                var series = ContinuedFractions.GetContinuedFractions(S);
                if ((series.Count - 1) % 2 != 0)
                {
                    ++oddPeriod;
                }

                //var m = 0;
                //var d = 1;
                //var sqS = Math.Sqrt(S);
                //var a = (int)Math.Floor(sqS);
                //Tuple<int, int, int> key;
                //fractions.Clear();
                //do
                //{
                //    m = d * a - m;
                //    d = (S - m * m) / d;

                //    // If this is a square then d will be zero
                //    if (d == 0)
                //        break;

                //    a = (int)Math.Floor((sqS + m) / d);
                //    key = new Tuple<int, int, int>(m, d, a);
                //    if (fractions.Contains(key))
                //        break;
                //    fractions.Add(key);
                //} while (true);

                //if (fractions.Count % 2 != 0)
                //{
                //    ++oddPeriod;
                //}
            }

            return Convert.ToString(oddPeriod);
        }
    }
}
