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
    class Problem115 : IProblem
    {
        public string Description
        {
            get { return "Finding a generalisation for the number of ways to fill a row with separated blocks."; }
        }

        public string Solve()
        {
            long[] cache = new long[500];
            long answer = 0;
            long rowSize = 49;
            do
            {
                ++rowSize;

                if (rowSize >= 500)
                    break;

                // Reset the cache
                for (int i = 0; i < cache.Length; i++)
                    cache[i] = -1;
                cache[50] = 1;

                // Find the total for this block size
                answer = 1 + Ways(50, rowSize, cache);
            } while (answer < 1000000);

            return Convert.ToString(rowSize);
        }

        private long Ways(long minSize, long blockSize, long[] ways)
        {
            if (blockSize < minSize)
                return 0;

            if (ways[blockSize] != -1)
                return ways[blockSize];

            long total = 0;
            for (long len = minSize; len <= blockSize; len++)
            {
                total += blockSize - len + 1;
                long maxpos = blockSize - len - 1;
                for (long pos = minSize; pos <= maxpos; pos++)
                {
                    total += Ways(minSize, pos, ways);
                }
            }

            ways[blockSize] = total;
            return total;
        }
    }
}
