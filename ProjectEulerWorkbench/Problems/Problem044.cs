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
    class Problem044 : IProblem
    {
        public string Description
        {
            get { return "Find the smallest pair of pentagonal numbers whose sum and difference is pentagonal."; }
        }

        long _n = 0;
        List<long> _pentagonals = new List<long>();
        HashSet<long> _pentagonalSet = new HashSet<long>();
        Queue<long> _nextTestValue = new Queue<long>();

        public string Solve()
        {
            long largestValue = 0;
            do
            {
                largestValue = NextPentagonal(false);
            } while (_n < 10000);

            int range = 1;
            int index1 = 0;
            int index2 = range;
            do
            {
                long p1 = _pentagonals[index1];
                long p2 = _pentagonals[index2];

                long sum = p1 + p2;
                long diff = p2 - p1;

                if (sum > largestValue)
                {
                    ++range;
                    index1 = 0;
                    index2 = range;
                    continue;
                }

                if (_pentagonalSet.Contains(sum) && _pentagonalSet.Contains(diff))
                {
                    // Found the solution!
                    return Convert.ToString(diff);
                }

                ++index1;
                index2 = index1 + range;

                if (index2 >= _pentagonals.Count)
                {
                    ++range;
                    index1 = 0;
                    index2 = range;
                }

            } while (range < _pentagonals.Count);

            return String.Empty;

            //bool pentagonalSum = false;
            //bool pentagonalDifference = false;
            //long p1;
            //long p2;
            //do
            //{
            //    pentagonalSum = false;
            //    pentagonalDifference = false;

            //    p1 = NextTestValue();
            //    p2 = NextTestValue();
            //    long sum = p1 + p2;
            //    long p3 = 0;

            //    if (_pentagonals.Contains(sum))
            //        pentagonalSum = true;
            //    else
            //    {
            //        do
            //        {
            //            p3 = NextPentagonal();
            //        } while (p3 < sum);
            //        if (p3 == sum)
            //            pentagonalSum = true;
            //    }

            //    if (pentagonalSum)
            //    {
            //        pentagonalSum = true;
            //        long difference = p2 - p1;
            //        if (difference > p3)
            //            do
            //            {
            //                p3 = NextPentagonal();
            //            } while (p3 < difference);
            //        else if (_pentagonals.Contains(difference))
            //            pentagonalDifference = true;
            //    }

            //} while (!(pentagonalSum && pentagonalDifference));

            //return Convert.ToString(p2 - p1);
        }

        private long NextTestValue()
        {
            if (_nextTestValue.Count == 0)
                return NextPentagonal(false);
            else
                return _nextTestValue.Dequeue();
        }

        private long NextPentagonal()
        {
            return NextPentagonal(true);
        }

        private long NextPentagonal(bool enqueue)
        {
            ++_n;
            long value = _n * (3 * _n - 1) / 2;
            _pentagonals.Add(value);
            _pentagonalSet.Add(value);
            if (enqueue)
                _nextTestValue.Enqueue(value);
            return value;
        }
    }
}
