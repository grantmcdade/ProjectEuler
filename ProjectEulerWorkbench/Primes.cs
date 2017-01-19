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
    sealed class Primes
    {
        private Primes()
        {
            // Empty prive constructor to prevent this class from being instantiated
        }

        public static bool MillerRabin(ulong n)
        {
            ulong[] ar;
            if (n < 4759123141) ar = new ulong[] { 2, 7, 61 };
            else if (n < 341550071728321) ar = new ulong[] { 2, 3, 5, 7, 11, 13, 17 };
            else ar = new ulong[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
            ulong d = n - 1;
            int s = 0;
            while ((d & 1) == 0) { d >>= 1; s++; }
            int i, j;
            for (i = 0; i < ar.Length; i++)
            {
                ulong a = Math.Min(n - 2, ar[i]);
                ulong now = pow(a, d, n);
                if (now == 1) continue;
                if (now == n - 1) continue;
                for (j = 1; j < s; j++)
                {
                    now = mul(now, now, n);
                    if (now == n - 1) break;
                }
                if (j == s) return false;
            }
            return true;
        }

        private static ulong mul(ulong a, ulong b, ulong mod)
        {
            int i;
            ulong now = 0;
            for (i = 63; i >= 0; i--) if (((a >> i) & 1) == 1) break;
            for (; i >= 0; i--)
            {
                now <<= 1;
                while (now > mod) now -= mod;
                if (((a >> i) & 1) == 1) now += b;
                while (now > mod) now -= mod;
            }
            return now;
        }

        private static ulong pow(ulong a, ulong p, ulong mod)
        {
            if (p == 0) return 1;
            if (p % 2 == 0) return pow(mul(a, a, mod), p / 2, mod);
            return mul(pow(a, p - 1, mod), a, mod);
        }
    }
}
