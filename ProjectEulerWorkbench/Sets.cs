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
    sealed class Sets
    {
        #region Constructor
        private Sets()
        {
            // Private constructor to prevent this class from being instantiated
        }
        #endregion

        #region Lexicographical Permutation Generator
        /// <summary>
        /// Generates each lexicographic permutation which can be made with the given
        /// array of digits and calls the handler deligate for each one
        /// </summary>
        /// <param name="digits">The array of digits</param>
        /// <param name="handler">The delegate to be called for each permutation</param>
        public static IEnumerable<int[]> GetLexicographicPermutations(int[] digits)
        {
            bool permutationFound = false;
            // The first set is a permutation so call the handler for that one immediately
            yield return digits;
            do
            {
                permutationFound = false;
                for (int k = digits.Length - 2; k >= 0; k--)
                {
                    if (digits[k] < digits[k + 1])
                    {
                        for (int l = digits.Length - 1; l >= 0; l--)
                        {
                            if (digits[k] < digits[l])
                            {
                                Util.Swap(digits, k, l);
                                Array.Reverse(digits, k + 1, digits.Length - (k + 1));
                                break;
                            }
                        }
                        permutationFound = true;
                        yield return digits;
                        break;
                    }
                }
            } while (permutationFound);
        }
        #endregion

        #region Binary Permutation Generator
        public static IEnumerable<T[]> GetBinaryPermutations<T>(T[] masterSet, int subsetSize)
        {
            if (masterSet.Length > 63)
                throw new Exception("Sets with more than 63 items are not supported!");

            var bitsHandled = 0;
            var length = (ulong)Math.Pow(2, masterSet.Length);
            var workingSet = new T[masterSet.Length];
            for (var i = 0UL; i < length; i++)
            {
                bitsHandled = 0;

                for (int j = 0; j < masterSet.Length; j++)
                {
                    var mask = 1UL << j;
                    if ((i & mask) == mask)
                    {
                        // If we hit the subset size limit while still processing this means
                        // the permutation is larger than the set limit we are imposing
                        if (subsetSize != 0 && bitsHandled >= subsetSize)
                        {
                            // Incriment the bits handled counter to flag this as an invalid subset
                            ++bitsHandled;
                            break;
                        }

                        workingSet[bitsHandled] = masterSet[j];
                        ++bitsHandled;
                    }
                }
                // Only return sets which match the subset size unless
                // a size of 0 was specified in which case all sets
                // must be returned
                if (subsetSize != 0 && bitsHandled != subsetSize)
                    continue;

                var result = new T[bitsHandled];
                Array.Copy(workingSet, result, bitsHandled);
                yield return result;
            }
        }
        #endregion

        #region ToNumber
        /// <summary>
        /// Converts an array of digits to a number
        /// </summary>
        /// <param name="digits">The array of digits to convert</param>
        /// <returns>A number composed of the digits in the array</returns>
        public static ulong ToNumber(int[] digits)
        {
            ulong number = 0;
            ulong multiplier = 1;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                number += (ulong)digits[i] * multiplier;
                multiplier *= 10;
            }
            return number;
        }
        #endregion

        #region ToDigits
        /// <summary>
        /// Gets the number of digits in the given number
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int DigitCount(ulong x)
        {
            // This bit could be optimised with a binary search
            return x < 10 ? 1
                 : x < 100 ? 2
                 : x < 1000 ? 3
                 : x < 10000 ? 4
                 : x < 100000 ? 5
                 : x < 1000000 ? 6
                 : x < 10000000 ? 7
                 : x < 100000000 ? 8
                 : x < 1000000000 ? 9
                 : x < 10000000000 ? 10
                 : x < 100000000000 ? 11
                 : x < 1000000000000 ? 12
                 : x < 10000000000000 ? 13
                 : x < 100000000000000 ? 14
                 : x < 1000000000000000 ? 15
                 : x < 10000000000000000 ? 16
                 : x < 100000000000000000 ? 17
                 : x < 1000000000000000000 ? 18
                 : x < 10000000000000000000 ? 19
                 : 20;
        }

        /// <summary>
        /// Gets the number of digits in the given number
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int DigitCount(int x)
        {
            // This bit could be optimised with a binary search
            return x < 10 ? 1
                 : x < 100 ? 2
                 : x < 1000 ? 3
                 : x < 10000 ? 4
                 : x < 100000 ? 5
                 : x < 1000000 ? 6
                 : x < 10000000 ? 7
                 : x < 100000000 ? 8
                 : x < 1000000000 ? 9
                 : 10;
        }

        /// <summary>
        /// Get an array of digits for the given number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int[] ToDigits(ulong number)
        {
            int size = DigitCount(number);
            int[] digits = new int[size];
            for (int index = size - 1; index >= 0; index--)
            {
                digits[index] = (int)(number % 10);
                number = number / 10;
            }
            return digits;
        }

        /// <summary>
        /// Get an array of digits for the given number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int[] ToDigits(int number)
        {
            int size = DigitCount(number);
            int[] digits = new int[size];
            for (int index = size - 1; index >= 0; index--)
            {
                digits[index] = (int)(number % 10);
                number = number / 10;
            }
            return digits;
        }

        /// <summary>
        /// Converts a number into an array of it's digits
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Obsolete("This method is slow and was replaced, don't use it")]
        public static int[] ToDigitsSlow(ulong number)
        {
            var digits = new List<int>();

            if (number < 10)
            {
                digits.Add((int)number);
                return digits.ToArray();
            }

            ulong value = number;
            ulong mask = 1;
            do
            {
                mask *= 10;
            } while (mask < value);
            mask /= 10;

            do
            {
                ulong r = value % mask;
                digits.Add((int)((value - r) / mask));
                value = r;
                mask /= 10;
            } while (mask > 1);
            digits.Add((int)value);

            return digits.ToArray();
        }
        #endregion

        #region BitsSet
        /// <summary>
        /// Returns the number of bits that are set
        /// </summary>
        /// <param name="value">The number to test</param>
        /// <returns>The count of bits set in number</returns>
        public static int BitsSet(ulong value)
        {
            int c; // c accumulates the total bits set in v
            for (c = 0; value > 0; c++)
            {
                value &= value - 1; // clear the least significant bit set
            }
            return c;
        }

        /// <summary>
        /// Returns the number of bits that are set
        /// </summary>
        /// <param name="value">The number to test</param>
        /// <returns>The count of bits set in number</returns>
        public static int BitsSet(uint value)
        {
            int c; // c accumulates the total bits set in v
            for (c = 0; value > 0; c++)
            {
                value &= value - 1; // clear the least significant bit set
            }
            return c;
        }
        #endregion
    }
}
