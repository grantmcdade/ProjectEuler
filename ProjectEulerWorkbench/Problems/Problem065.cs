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
    class Problem065 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of digits in the numerator of the 100th convergent of the continued fraction for e."; }
        }

        public string Solve()
        {
            const int limit = 100;
            var sequence = new int[limit];
            sequence[0] = 2;
            sequence[1] = 1;
            sequence[2] = 2;
            for (int i = 3; i < limit; i++)
                sequence[i] = 1;
            for (int i = 5; i < limit; i += 3)
                sequence[i] = sequence[i - 3] + 2;

            //BigInteger a = 66UL;
            //BigInteger d = 2UL;
            //BigInteger n = 1UL;
            //for (int i = 1; i < limit; i++)
            //{
            //    d = sequence[i];
            //    n = 1UL;
            //    for (int j = i - 1; j >= 0; j--)
            //    {
            //        a = sequence[j];
            //        // Calculate the new n
            //        n = a * d + n;
            //        // Swap the numerator and the denominator
            //        d = n + d - (n = d);
            //    }
            //}

            var answer = ContinuedFractions.ResolveContinuedFraction(new List<int>(sequence), 1);
            var numerator = answer.Item1.ToString();
            return Convert.ToString(numerator.Sum(c => c - 48));

            //var numerator = d.ToString();

            //return Convert.ToString(numerator.Sum(c => c - 48));
        }
    }
}
