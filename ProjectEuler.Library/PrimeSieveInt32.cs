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
using System.Collections;

namespace ProjectEuler.Library
{
    public class PrimeSieveInt32
    {
        private BitArray _primes;
        private float[] _totients;

        /// <summary>
        /// Set to true if the sive should also prepare a list of totient values
        /// </summary>
        public bool IncludeTotients { get; set; }

        public int Limit { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PrimeSieveInt32()
        {
            Limit = 0;
            IncludeTotients = false;
        }

        /// <summary>
        /// Initialise the sieve for all primes up to the specified limit
        /// </summary>
        /// <param name="limit">Include primes up to this limit</param>
        public void Initialise(int limit)
        {
            Limit = limit;

            // Initialise the prime sieve
            _primes = new BitArray(limit + 1, false);
            _primes[2] = true;
            for (int i = 3; i <= limit; i += 2)
                _primes[i] = true;
            for (int i = 3; i <= limit; i += 2)
                if (_primes[i])
                    for (int j = i + i; j <= limit; j += i)
                        _primes[j] = false;

            // Initialise the totients
            if (IncludeTotients)
            {
                // Thanks to Josay from Project Euler, this totient sieve code was posted
                // by him for problem 72 with the comment "A blink of an eye in C". He was
                // right, it's really fast! With a limit of 10 million, primes and totients
                // are generated in 1.5 seconds.

                // Initialise the totient array for sieving
                _totients = new float[limit + 1];
                for (int i = 1; i <= limit; i++)
                    _totients[i] = i;

                for (int i = 2; i <= limit; i++)
                    if (_primes[i])
                        for (int j = i; j <= limit; j += i)
                            _totients[j] *= 1.0f - (1.0f / i);
            }
            else
            {
                _totients = null;
            }
        }

        /// <summary>
        /// Check if the specified number is a prime number
        /// </summary>
        /// <param name="p">The number to check</param>
        /// <returns>True if the number is prime</returns>
        public bool IsPrime(int p)
        {
            return _primes[p];
        }

        /// <summary>
        /// Get the Euler Totient for the specified number
        /// </summary>
        /// <param name="n"></param>
        /// <returns>The totient of the specified number</returns>
        public int Phi(int n)
        {
            return (int)_totients[n];
        }

        /// <summary>
        /// Get an enumerator of all the prime numbers produced by this sieve
        /// </summary>
        /// <returns>The prime numbers below the limit specified during initilisation</returns>
        public IEnumerable<int> GetPrimes()
        {
            for (int i = 2; i < _primes.Length; i++)
            {
                if (_primes[i])
                {
                    yield return i;
                }
            }
        }
    }
}
