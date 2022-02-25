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

namespace ProjectEuler.Library
{
    public interface IPrimeSieveInt32
    {
        /// <summary>
        /// Get an enumerator of all the prime numbers produced by this sieve
        /// </summary>
        /// <returns>The prime numbers below the limit specified during initilisation</returns>
        IEnumerable<int> GetPrimes();

        /// <summary>
        /// Initialise the sieve for all primes up to the specified limit
        /// </summary>
        /// <param name="limit">Include primes up to this limit</param>
        void Initialise(int limit);

        /// <summary>
        /// Check if the specified number is a prime number
        /// </summary>
        /// <param name="p">The number to check</param>
        /// <returns>True if the number is prime</returns>
        bool IsPrime(int p);

        /// <summary>
        /// Get the Euler Totient for the specified number
        /// </summary>
        /// <param name="n"></param>
        /// <returns>The totient of the specified number</returns>
        int Phi(int n);

        /// <summary>
        /// Set to true if the sieve should also prepare a list of totient values
        /// </summary>
        bool IncludeTotients { get; set; }
        int Limit { get; }
    }
}
