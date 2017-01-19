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

namespace ProjectEulerWorkbench
{
    class ContinuedFractions
    {
        public static List<int> GetContinuedFractions(int S)
        {
            var m = 0;
            var d = 1;
            var sqS = Math.Sqrt(S);
            var a = (int)Math.Floor(sqS);
            Tuple<int, int, int> key;
            var series = new List<int>();
            series.Add(a);
            var fractions = new HashSet<Tuple<int, int, int>>();
            do
            {
                m = d * a - m;
                d = (S - m * m) / d;

                // If this is a square then d will be zero
                if (d == 0)
                    break;

                a = (int)Math.Floor((sqS + m) / d);
                key = new Tuple<int, int, int>(m, d, a);
                if (fractions.Contains(key))
                    break;
                fractions.Add(key);
                series.Add(a);
            } while (true);

            return series;
        }

        public static Tuple<BigInteger, BigInteger> ResolveContinuedFraction(List<int> series, int convergent)
        {
            if (series.Count < 2)
                return null;

            if (convergent < 1)
                return null;

            if (convergent == 1)
                return new Tuple<BigInteger, BigInteger>(series[0], 1);

            // If the convergent is larger than our series then we need
            // to grow the series with the assumption that the period is
            // one minus the length of the series.
            var length = series.Count;
            while (convergent > series.Count)
                for (int i = 1; i < length; i++)
                    series.Add(series[i]);

            BigInteger a = 0, d = 0, n = 0;
            //for (int i = 1; i < series.Count; i++)
            //{
            d = series[convergent - 1];
            n = 1;
            for (int j = convergent - 2; j >= 0; j--)
            {
                a = series[j];
                // Calculate the new n
                n = a * d + n;
                // Swap the numerator and the denominator
                d = n + d - (n = d);
            }
            //}
            return new Tuple<BigInteger, BigInteger>(d, n);
        }
    }
}
