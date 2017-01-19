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
    class Problem114 : IProblem
    {
        public string Description
        {
            get { return "Investigating the number of ways to fill a row with separated blocks that are at least three units long."; }
        }

        public string Solve()
        {
            // Answer is: 16475640049
            // For 8 elements it's: 27
            // For 9 elements it's: 44

            //const long target = 9;

            //long ways = 1;
            //long minSize = 3;
            //for (long currentSize = minSize; currentSize <= target; currentSize++)
            //{
            //    ways += Ways(minSize, currentSize, 1, target);
            //}

            //return Convert.ToString(ways);

            long[] cache = new long[51];
            for (int i = 0; i < cache.Length; i++)
                cache[i] = -1;

            cache[3] = 1;

            long answer = 1 + Ways2(50, cache);
            return Convert.ToString(answer);
        }

        private long Ways2(long blockSize, long[] ways)
        {
            if (blockSize < 3)
                return 0;

            if (ways[blockSize] != -1)
                return ways[blockSize];

            long total = 0;
            for (long len = 3; len <= blockSize; len++)
            {
                total += blockSize - len + 1;
                long maxpos = blockSize - len - 1;
                for (int pos = 0; pos <= maxpos; pos++)
                {
                    total += Ways2(pos, ways);
                }
            }

            ways[blockSize] = total;
            return total;
        }

        /// <summary>
        /// Calculate the number of ways a fixed length tile can be placed on the
        /// target space
        /// </summary>
        /// <param name="currentSize">The fixed length tile size</param>
        /// <param name="target">The size of the target space</param>
        /// <returns>Number of combinations</returns>
        private long Ways(long minSize, long currentSize, long blockCount, long target)
        {
            // Simply return if the block to place is larger than the target space
            if (currentSize > target)
                return 0;

            // If the block is the same size as the target space there is only one permutation
            if (currentSize == target)
                return 1;

            // Place the tile once and remove the affected blocks from the target area, then remove one block for the spacer needed between blocks
            long ways = 1;
            long blocksLeft = target - currentSize;

            if (blocksLeft > 0 && blocksLeft <= minSize)
            {
                // If there are blocks left then for every block remaining we have a permutation which
                // involves moving all placed blocks over one at a time, by one block at a time. This
                // does not count for the current block as we have already accounted for that above.
                ways += (blockCount - 1) * blocksLeft;
            }

            blocksLeft -= 1;

            // After placing a tile there are more combinations up to the length of
            // the remaining target space since we can move the tile one block each time
            ways += target - currentSize;

            // We can then recursively find the number of ways the remaining space
            // can be used
            if (blocksLeft >= minSize)
            {
                for (long i = minSize; i <= blocksLeft; i++)
                    ways += Ways(minSize, i, blockCount + 1, blocksLeft);
            }

            return ways;
        }


        /////////////////////////////////////////////////////////////////////
        // http://keyzero.wordpress.com/2010/05/20/project-euler-problem-114/
        //
        private long Count(long gap, long[] cache)
        {
            if (gap < 3) return 0;
            if (cache[gap] != -1) return cache[gap];

            long total = 0;
            for (long len = 3; len <= gap; ++len)
            {
                total += gap - len + 1;
                long maxpos = gap - len + 1;
                for (long pos = 0; pos <= maxpos; ++pos)
                {
                    total += Count(gap - len - pos - 1, cache);
                }
            }
            cache[gap] = total;
            return total;
        }

        private void AttemptTwo()
        {
            // This calculates the number of integer partitions it does however not allow for composition
            // and therefore does not contain all the possible combinations. I have yet to figure out
            // how to modify the algorithm to allow the additional combinations introduced through
            // composition.
            const long target = 7;
            long[] ways = new long[target + 1];
            ways[0] = 1;
            for (int i = 1; i < target; i++)
            {
                if (i == 2) // Skip 2 as the rules of the blocks exclude this from the set
                    continue;
                for (int j = i; j <= target; j++)
                {
                    ways[j] += ways[j - i];
                }
            }
        }

        private void TheWrongWay()
        {
            // This works great for a set of 7 however it would take more than 35 thousand years
            // to run it for a set of 50 elements
            long length = 0x4000000000000;
            int setCount = 0;
            for (long i = 0; i < length; i++)
            {
                // Check if the bit pattern match the rules for packing
                // reject all numbers with a bit pattern where there are
                // less than three consecutive 1s.
                int consecutiveBitsSet = 0;
                bool rejectNumber = false;
                for (int j = 0; j < 50; j++)
                {
                    int mask = 1 << j;
                    if ((mask & i) == mask)
                    {
                        ++consecutiveBitsSet;
                    }
                    else if (consecutiveBitsSet != 0 && consecutiveBitsSet < 3)
                    {
                        rejectNumber = true;
                        break;
                    }
                    else if (consecutiveBitsSet != 0)
                    {
                        consecutiveBitsSet = 0;
                    }
                }
                if (!rejectNumber)
                    ++setCount;
            }
        }
    }
}
