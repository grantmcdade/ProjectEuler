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
    class Problem033 : IProblem
    {
        public string Description
        {
            get { return "Discover all the fractions with an unorthodox cancelling method."; }
        }

        public string Solve()
        {
            var curiousFractions = new List<Tuple<int, int>>();
            for (int n = 10; n < 100; n++)
            {
                for (int d = n + 1; d < 100; d++)
                {
                    var fraction = (Decimal)n / (Decimal)d;

                    var nd = Convert.ToString(n);
                    var dd = Convert.ToString(d);
                    string ndn = nd, ddn = dd;
                    foreach (var digit in nd)
                    {
                        if (digit != '0' && dd.Contains(digit))
                        {
                            ndn = ndn.Trim(new char[] { digit });
                            ddn = ddn.Trim(new char[] { digit });
                        }
                    }
                    if (ndn.Length == 1 && ddn.Length == 1 && ndn != "0" && ddn != "0")
                    {
                        var newFraction = Decimal.Parse(ndn) / Decimal.Parse(ddn);
                        if (fraction == newFraction)
                        {
                            // Curious fraction found!
                            curiousFractions.Add(new Tuple<int, int>(n, d));
                        }
                    }
                }
            }

            int numerator = 1;
            int denominator = 1;
            foreach (var item in curiousFractions)
            {
                numerator *= item.Item1;
                denominator *= item.Item2;
            }

            // The numerator is an exact factor of the denominator so we can just
            // divide to get the new denominator in it's lowest form
            return Convert.ToString(denominator / numerator);
        }
    }
}
