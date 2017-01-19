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

using System.Collections;
using System.Numerics;

namespace ProjectEulerWorkbench
{
    static class Extensions
    {
        public static BigInteger ToBigInteger(this BitArray candidates)
        {
            //BigInteger result = 0;
            //BigInteger next = 1;
            //for (int i = 0; i < candidates.Length; i++)
            //{
            //    if (candidates.Get(i))
            //    {
            //        result += next;
            //    }
            //    next <<= 1;
            //}

            byte[] value = new byte[candidates.Length / 8 + 1];
            candidates.CopyTo(value, 0);
            return new BigInteger(value);
        }


        public static int ToInt32(this BitArray candidates)
        {
            int result = 0;
            int next = 1;
            for (int i = 0; i < candidates.Length; i++)
            {
                if (candidates[i])
                {
                    result += next;
                }
                next = next << 1;
            }
            return result;
        }
    }
}
