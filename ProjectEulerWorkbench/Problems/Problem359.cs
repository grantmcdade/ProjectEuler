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
    class Problem359 : IProblem
    {
        public string Description
        {
            get { return "Hilbert's New Hotel"; }
        }

        //private BigInteger[] _squares;

        public string Solve()
        {
            //const long limit = 71328803586048;
            const long limit = 7132;
            // var factors = Util.GetFactors(limit);

            //_squares = new BigInteger[1000];
            //for (int i = 0; i < _squares.Length; i++)
            //{
            //    _squares[i] = i * i;
            //}

            //var rooms = new List<List<int>>();
            //for (int i = 1; i < 1000; i++)
            //{
            //    bool roomFound = false;
            //    for (int j = 0; j < rooms.Count; j++)
            //    {
            //        var sq = i + rooms[j][rooms[j].Count - 1];
            //        if (sq.IsPerfectSquare())
            //        {
            //            rooms[j].Add(i);
            //            roomFound = true;
            //            break;
            //        }
            //    }
            //    if (!roomFound)
            //    {
            //        rooms.Add(new List<int>());
            //        rooms[rooms.Count - 1].Add(i);
            //    }
            //}

            //for (int i = 0; i < rooms.Count; i++)
            //{
            //    for (int j = 0; j < rooms[i].Count; j++)
            //    {
            //        System.Diagnostics.Debug.Write(rooms[i][j]);
            //        System.Diagnostics.Debug.Write(",");
            //    }
            //    System.Diagnostics.Debug.WriteLine(String.Empty);
            //}

            //for (int i = 0; i < rooms.Count; i++)
            //{
            //    System.Diagnostics.Debug.Write(rooms[i][0]);
            //    System.Diagnostics.Debug.Write(",");
            //}
            //System.Diagnostics.Debug.WriteLine(String.Empty);

            //var a = P(1, 1); // = 1
            //a = P(1, 2); // = 3 
            //a = P(2, 1); // = 2 
            //var a = P(10, 20); // = 440 
            //a = P(25, 75); // = 4863 
            //a = P(99, 100); // = 19454

            BigInteger sum = 0;
            var sq = (long)Math.Sqrt(limit);
            for (long i = 1; i <= sq; i++)
            {
                if (limit % i == 0)
                {
                    var f = i;
                    var r = limit / f;
                    sum += P(f, r);
                }
            }

            return Convert.ToString(sum);
        }

        public BigInteger P(long f, long r)
        {
            if (f == 1 && r == 1)
                return 1;

            // Calculate the starting number for the floor
            BigInteger start = (long)Math.Floor((f * f) / 2d);
            if (start == 0)
                start = 1;

            BigInteger room = f;
            if (f % 2 == 0)
                ++room;
            for (long i = 1; i < r; i++)
            {
                start = (room * room) - start;
                ++room;
            }

            // Calculate the square sequence to find the person
            // in room r
            //var room = 1;
            //while (room < r)
            //{

            //    ++room;
            //    var val = (long)Math.Sqrt(start + start) + 1;
            //    start = (val * val) - start;

            //var test = start + start + 1;
            //while (!test.IsPerfectSquare())
            //{
            //    ++test;
            //}
            //start = test - start;
            //}

            return start;
        }

        public static BigInteger SqRtN(BigInteger N)
        {
            /*++
             *  Using Newton Raphson method we calculate the
             *  square root (N/g + g)/2
             */
            BigInteger rootN = N;
            int count = 0;
            int bitLength = 1; // There is a bug in finding bit length hence we start with 1 not 0
            while (rootN / 2 != 0)
            {
                rootN /= 2;
                bitLength++;
            }
            bitLength = (bitLength + 1) / 2;
            rootN = N >> bitLength;

            BigInteger lastRoot = BigInteger.Zero;
            do
            {
                if (lastRoot > rootN)
                {
                    if (count++ > 1000)                   // Work around for the bug where it gets into an infinite loop
                    {
                        return rootN;
                    }
                }
                lastRoot = rootN;
                rootN = (BigInteger.Divide(N, rootN) + rootN) >> 1;
            }
            while (!((rootN ^ lastRoot).ToString() == "0"));
            return rootN;
        } // SqRtN    

    }
}
