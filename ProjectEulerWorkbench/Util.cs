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
using System.Numerics;
using System.IO;
using System.Reflection;
using System.Collections;

using variable = System.UInt64; // Commmon place to change the type of 'variable'
using Euclid = System.Tuple<System.UInt64, System.UInt64, System.UInt64>;

namespace ProjectEulerWorkbench.Problems
{
    sealed class Util
    {

        #region Constructors
        private Util()
        {
            // Private constructor to prevent this class from being instantiated
        }
        #endregion

        #region IsPrime
        public static bool IsPrime(int value)
        {
            if (value <= 1)
                return false;

            var sqrt = (int)Math.Sqrt(value);
            for (int i = 2; i <= sqrt; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }

        public static bool IsPrime(long value)
        {
            if (value <= 1)
                return false;

            var sqrt = (long)Math.Sqrt(value);
            for (long i = 2; i <= sqrt; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }

        public static bool IsPrime(variable value)
        {
            if (value <= 1)
                return false;

            var sqrt = (variable)Math.Sqrt(value);
            for (variable i = 2; i <= sqrt; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }

        public static bool IsPrime(variable value, List<variable> primes)
        {
            var max = Math.Sqrt(value);
            // First test against all the known prime numbers
            variable largestPrime = 2;
            foreach (var item in primes)
            {
                if (value % item == 0) return false;
                if (item > largestPrime) largestPrime = item;
                if (largestPrime > max) return true;
            }

            // Then test against the remaining numbers
            for (variable i = largestPrime + 1; i <= max; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }
        #endregion

        #region Seive of Atkin
        public static variable[] SieveOfAtkinInt64(variable limit)
        {
            var sqrt = Math.Sqrt(limit);
            bool[] isPrime = new bool[limit + 1];

            // Put in candidate primes
            for (variable x = 1; x <= sqrt; x++)
            {
                for (variable y = 1; y <= sqrt; y++)
                {
                    var n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                    {
                        isPrime[n] ^= true;
                    }

                    n = 3 * x * x + y * y;
                    if (n <= limit && (n % 12 == 7))
                    {
                        isPrime[n] ^= true;
                    }

                    if (x > y)
                    {
                        n = 3 * x * x - y * y;
                        if (n <= limit && (n % 12 == 11))
                        {
                            isPrime[n] ^= true;
                        }
                    }
                }
            }

            // Eliminate composites
            for (variable n = 5; n <= sqrt; n++)
            {
                if (isPrime[n])
                {
                    var s = n * n;
                    for (variable k = s; k < limit; k += s)
                        isPrime[k] = false;
                }
            }

            // Transfer the result to an array
            List<variable> primes = new List<variable>
            {
                2,
                3
            };
            for (variable i = 5; i <= limit; i++)
            {
                if (isPrime[i]) primes.Add(i);
            }
            return primes.ToArray();
        }

        public static int[] SieveOfAtkinInt32(int limit)
        {
            var sqrt = (int)Math.Sqrt(limit);
            var isPrime = new BitArray(limit + 1);

            // Put in candidate primes
            for (var x = 1; x <= sqrt; x++)
            {
                for (var y = 1; y <= sqrt; y++)
                {
                    var n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                    {
                        isPrime[n] ^= true;
                    }

                    n = 3 * x * x + y * y;
                    if (n <= limit && (n % 12 == 7))
                    {
                        isPrime[n] ^= true;
                    }

                    if (x > y)
                    {
                        n = 3 * x * x - y * y;
                        if (n <= limit && (n % 12 == 11))
                        {
                            isPrime[n] ^= true;
                        }
                    }
                }
            }

            // Eliminate composites
            for (var n = 5; n <= sqrt; n++)
            {
                if (isPrime[n])
                {
                    var s = n * n;
                    for (var k = s; k < limit; k += s)
                        isPrime[k] = false;
                }
            }

            // Transfer the result to an array
            var primes = new List<int>
            {
                2,
                3
            };
            for (var i = 5; i <= limit; i++)
            {
                if (isPrime[i]) primes.Add(i);
            }
            return primes.ToArray();
        }

        #endregion

        #region Generate Prime Numbers
        public static variable[] GetFirstNPrimeNumbers(variable limit)
        {
            variable n = 2;
            List<variable> primes = new List<variable>();
            do
            {
                if (Util.IsPrime(n)) { primes.Add(n); }
                n += 1;
            } while ((variable)primes.Count < limit);
            return primes.ToArray();
        }

        public static variable[] GetPrimeNumbersLessThan(variable value)
        {
            variable testValue = 2;
            List<variable> primes = new List<variable>();
            do
            {
                if (Util.IsPrime(testValue, primes)) { primes.Add(testValue); }
                testValue += 1;
            } while (testValue <= value);
            return primes.ToArray();
        }
        #endregion

        #region Get Divisors
        public static variable[] GetFactors(variable value)
        {
            var workingSet = new HashSet<variable>();
            var sqrt = (ulong)Math.Sqrt(value);
            for (ulong i = 1; i <= sqrt; i++)
            {
                if (value % i == 0) workingSet.Add(i);
            }
            var items = workingSet.ToArray();
            foreach (var item in items)
            {
                workingSet.Add(value / item);
            }
            var factors = workingSet.ToArray();
            Array.Sort(factors);
            return factors;
        }

        private static variable[] GetFactors_Obsolete(variable value)
        {
            HashSet<variable> workingSet = new HashSet<variable>();

            // Get all the prime factors for this number
            var primes = Util.SieveOfAtkinInt64(value / 2);
            foreach (var prime in primes)
            {
                if (value % prime == 0) workingSet.Add(prime);
            }

            var sqrt = Math.Sqrt(value);
            for (variable i = 2; i <= sqrt; ++i)
            {
                variable remainder = value % i;
                if (remainder == 0)
                    workingSet.Add(i);
            }

            variable[] primeFactors = new variable[workingSet.Count];
            workingSet.CopyTo(primeFactors);
            Array.Sort<variable>(primeFactors);

            // Calcualte all the possible factors by testing multiples of the prime factors
            for (variable i = 0; i < (variable)primeFactors.Length; i++)
            {
                var s = primeFactors[i] * 2;
                for (variable k = s; k < value; k += s)
                    if (value % k == 0) workingSet.Add(k);
            }

            // Ensure that the nubers 1 and the number itself are in the list
            workingSet.Add(1);
            workingSet.Add(value);

            variable[] factors = new variable[workingSet.Count];
            workingSet.CopyTo(factors);
            Array.Sort<variable>(factors);

            return factors;
        }
        #endregion

        #region Coprime
        /// <summary>
        /// Tests if two numbers are coprime, that is, thier greatest common divisor is 1
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>True if the numbers are a coprime pair</returns>
        public static bool Coprime(variable a, variable b)
        {
            // Variables a and b are co-prime if thier greatest common divisor is 1
            var factorsA = GetFactors(a);
            var factorsB = GetFactors(b);

            bool coprime = true;

            for (int i = 1; i < factorsA.Length; i++)
            {
                for (int j = i; j < factorsB.Length; j++)
                {
                    if (factorsA[i] == factorsB[j])
                    {
                        coprime = false;
                        break;
                    }
                }
                if (!coprime)
                    break;
            }

            return coprime;
        }

        /// <summary>
        /// Get the greatest common divisor of two numbers using the Euclidean algorithm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static variable Gcd(variable a, variable b)
        {
            if (a == b)
                return a;
            do
            {
                var t = b;
                b = a % b;
                a = t;
            } while (b != 0);
            return a;
        }

        public static int Gcd(int a, int b)
        {
            if (a == b)
                return a;
            do
            {
                var t = b;
                b = a % b;
                a = t;
            } while (b != 0);
            return a;
        }

        //public static int Totient(int value)
        //{
        //    var totient = 0;
        //    for (int i = 1; i < value; i++)
        //        if (Gcd(value, i) == 1)
        //            ++totient;
        //    return totient;
        //}

        /// <summary>
        /// Uses Euler's Totient function to calculate the totient
        /// </summary>
        /// <param name="n"></param>
        /// <param name="primes"></param>
        /// <returns></returns>
        public static int Phi(int n, int[] primes)
        {
            var complete = false;
            var totient = n;
            for (int i = 0; i < primes.Length; i++)
            {
                var p = primes[i];

                if (p > n)
                {
                    complete = true;
                    break;
                }

                if (n % p == 0)
                    totient -= totient / p;

            }

            if (!complete)
                throw new ApplicationException("Phi was not able to calculate the totient, the prime array is possibly too small");

            return totient;
        }

        #endregion

        #region Euclid Formula
        /// <summary>
        /// Euclids formula for generating a pythagorian triple
        /// </summary>
        /// <param name="m">Arbitrary positive integer with m > n</param>
        /// <param name="n">Arbitrary positive integer with m > n</param>
        /// <returns>A pythagorian triple</returns>
        public static Euclid Euclid(variable m, variable n)
        {
            if (m <= n) throw new ArgumentException("m > n");
            return new Euclid((m * m) - (n * n), 2 * m * n, (m * m) + (n * n));
        }
        #endregion

        #region Sum
        public static variable Sum(variable[] values)
        {
            return Sum(values, values.Length);
        }

        public static variable Sum(variable[] values, int length)
        {
            variable result = 0;
            // Protect against invalid input by checking the length and limiting
            // it to the array length
            if (length > values.Length)
                length = values.Length;
            for (int i = 0; i < length; i++)
            {
                result += values[i];
            }
            return result;
        }
        #endregion

        #region Working with digit arrays
        [Obsolete("Use the Stes.DigitCount method, it's faster")]
        public static int Length(variable value)
        {
            if (value < 10)
                return 1;

            if (value < 100)
                return 2;

            if (value < 1000)
                return 3;

            if (value < 10000)
                return 4;

            if (value < 100000)
                return 5;

            int digits = 0;
            variable upperBound = 1;
            while (upperBound <= value)
            {
                upperBound *= 10;
                ++digits;
            }
            return digits;
        }

        public static int Length(int value)
        {
            return 1 + Convert.ToInt32(Math.Floor(Math.Log10(value)));
        }

        [Obsolete("Use an array of digits from Sets.ToDigits, it's faster")]
        public static int Digit(variable value, int position)
        {
            // Extract the specified digit
            variable lowMask = 1;
            for (int i = 0; i < position; i++)
                lowMask *= 10;

            variable highMask = lowMask * 10;

            variable result = value % highMask;
            result = result - (result % lowMask);
            result /= lowMask;

            return (int)result;
        }
        #endregion

        #region Pow
        public static int Pow(int value, int pow)
        {
            int n = value;
            for (int i = 1; i < pow; i++)
                value *= n;
            return value;
        }

        public static variable Pow(variable value, int pow)
        {
            variable n = value;
            for (int i = 1; i < pow; i++)
                value *= n;
            return value;
        }

        public static variable Pow(variable value, variable pow)
        {
            variable n = value;
            for (variable i = 1; i < pow; i++)
                value *= n;
            return value;
        }
        #endregion

        #region Factorial
        public static BigInteger Factorial(BigInteger value)
        {
            if (value == 0 || value == 1)
                return 1;

            if (value == 2)
                return 2;

            BigInteger factorial = value;
            for (BigInteger i = value - 1; i > 0; i--)
            {
                factorial *= i;
            }
            return factorial;
        }
        #endregion

        #region Fast Swap
        public static void Swap(int[] digits, int x, int y)
        {
            digits[x] = digits[x] + digits[y] - (digits[y] = digits[x]);
        }
        #endregion

        #region Triangle, Pentagonal and Hexagonal Numbers
        public static bool IsTriangleNumber(int number)
        {
            double c = number * 2.0;
            for (double p = Math.Floor(Math.Sqrt(c)); p > 0; p--)
            {
                if ((c % p) == 0)
                {
                    double q = c / p;
                    if (Math.Abs(q - p) == 1)
                        return true;
                }
            }
            return false;
        }

        public static bool IsPentagonalNumber(int number)
        {
            double c = number * 2.0;
            for (double p = Math.Floor(Math.Sqrt(c)); p > 0; p--)
            {
                if ((c % p) == 0)
                {
                    double q = c / p;
                    if (Math.Abs(q - 3 * p) == 1.0)
                        return true;
                }
            }
            return false;
        }

        public static bool IsHexagonalNumber(int number)
        {
            double c = number;
            for (double p = Math.Floor(Math.Sqrt(c)); p > 0; p--)
            {
                if ((c % p) == 0)
                {
                    double q = c / p;
                    if (Math.Abs(q - 2 * p) == 1.0)
                        return true;
                }
            }
            return false;
        }
        #endregion

    }
}
