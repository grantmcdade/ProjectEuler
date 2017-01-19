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
    class Problem069 : IProblem
    {
        public string Description
        {
            get { return "Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum."; }
        }

        public string Solve()
        {
            const int length = 1000000;

            var primes = Util.SieveOfAtkinInt32(30);

            // Calculate the product of primes up to the limit
            var maxN = 1;
            for (int i = 0; i < primes.Length; i++)
            {
                var newProduct = maxN * primes[i];
                if (newProduct < length)
                    maxN = newProduct;
                else
                    break;
            }

            //var maxNPhi = 0.0d;
            //var maxN = 0;
            //for (int i = 2; i <= length; i++)
            //{
            //    var nphi = (double)i / (double)Phi(i, primes);
            //    if (nphi > maxNPhi)
            //    {
            //        maxNPhi = nphi;
            //        maxN = i;
            //    }
            //}
            return Convert.ToString(maxN);
        }
    }
}
