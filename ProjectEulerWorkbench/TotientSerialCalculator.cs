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

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ProjectEulerWorkbench
{
    public class TotientSerialCalculator : ITotientSerialCalculator
    {
        //Implements an extremely efficient Serial Totient(phi) calculator   '
        //  This implements an optimized windowed Sieve of Eratosthenes.  The'
        // window size is set at Sqrt(N) both to optimize collecting and     '
        // applying all of the Primes below Sqrt(N), and to minimize         '
        // window-turning overhead.                                          '
        //                                                                   '
        // CPU complexity is O( N * Log(Log(N)) ), which is virtually linear.'
        //                                                                   '
        // MEM Complexity is O( Sqrt(N) ).                                   '
        //                                                                   '
        // This is probably the ideal combination, as any attempt to further '
        //reduce memory will almost certainly result in disproportionate increases'
        //in CPU complexity, and vice-versa.                                 '

        public struct NumberFactors
        {
            //the part of the number that still needs to be factored'
            public long UnFactored;
            //the totient value progressively calculated'
            public long Phi;
            //               (equals total numbers less than N that are CoPrime to N)'
            //MEM = 8 bytes each'
        }

        private long ReportInterval;
        //the last value in the previous window'
        private long PrevLast;
        //the first value in this windows range'
        private long FirstValue;
        private long WindowSize;
        //the last value in this windows range'
        private long LastValue;
        //the first value in the next window'
        private long NextFirst;

        //Array that stores all of the NumberFactors in the current window.'
        // this is the primary memory consumption for the class and it'
        // is 16 * Sqrt(N) Bytes, which is O(Sqrt(N)).'
        public NumberFactors[] Numbers;
        // For N=10^12 (1 trillion), this will be 16MB, which should be bearable anywhere.'
        //(note that the Primes() array is a secondary memory consumer'
        //  at O(pi(Sqrt(N)), which will be within 10x of O(Sqrt(N)))'

        public event EmitTotientPairEventHandler EmitTotientPair;
        public delegate void EmitTotientPairEventHandler(object sender, TotientPairEventArgs e);

        //===== The Routine To Call: ========================'
        public void EmitTotientPairsToN(long N)
        {
            //Routine to Emit Totient pairs {k, Phi(k)} for k = 1 to N'
            //   2009-07-14, RBarryYoung, Created.'
            long i = 0;
            long k = 0;
            //the current number being factored'
            long p = 0;
            //the current prime factor'

            //Establish the Window frame:'
            //   note: WindowSize is the critical value that controls both memory'
            //    usage and CPU consumption and must be SQRT(N) for it to work optimally.'
            WindowSize = (long)Math.Ceiling(Math.Sqrt(Convert.ToDouble(N)));
            Numbers = new NumberFactors[WindowSize];

            //Initialize the first window:'
            MapWindow(1);
            bool IsFirstWindow = true;

            //adjust this to control how often results are show'
            ReportInterval = N / 100;

            //Allocate the primes array to hold the primes list:'
            //  Only primes <= SQRT(N) are needed for factoring'
            //  PiMax(X) is a Max estimate of the number of primes <= X'
            long[] Primes = null;
            long PrimeIndex = 0;
            long NextPrime = 0;
            //init the primes list and its pointers'
            Primes = new long[PiMax(WindowSize)];
            Primes[0] = 2;
            //"prime" the primes list with the first prime'
            NextPrime = 1;

            //Map (and Remap) the window with Sqrt(N) numbers, Sqrt(N) times to'
            // sequentially map all of the numbers <= N.'
            do
            {
                //Sieve the primes across the current window'
                PrimeIndex = 0;
                //note: cant use enumerator for the loop below because NextPrime'
                // changes during the first window as new primes <= SQRT(N) are accumulated'
                while (PrimeIndex < NextPrime)
                {
                    //get the next prime in the list'
                    p = Primes[PrimeIndex];
                    //find the first multiple of (p) in the current window range'
                    k = PrevLast + p - (PrevLast % p);

                    do
                    {
                        // var _with1 = Numbers[k - FirstValue];
                        Numbers[k - FirstValue].UnFactored = Numbers[k - FirstValue].UnFactored / p;
                        //always works the first time'
                        Numbers[k - FirstValue].Phi = Numbers[k - FirstValue].Phi * (p - 1);
                        //Phi = PRODUCT( (Pi-1)*Pi^(Ei-1) )'
                        //The loop test that follows is probably the central CPU overhead'
                        // I believe that it is O(N*Log(Log(N)), which is virtually O(N)'
                        // ( for instance at N = 10^12, Log(Log(N)) = 3.3 )'
                        while ((Numbers[k - FirstValue].UnFactored % p) == 0)
                        {
                            Numbers[k - FirstValue].UnFactored = Numbers[k - FirstValue].UnFactored / p;
                            Numbers[k - FirstValue].Phi = Numbers[k - FirstValue].Phi * p;
                        }

                        //skip ahead to the next multiple of p: '
                        //(this is what makes it so fast, never have to try prime factors that dont apply)'
                        k += p;
                        //repeat until we step out of the current window:'
                    } while (k < NextFirst);

                    //if this is the first window, then scan ahead for primes'
                    if (IsFirstWindow)
                    {
                        //the range of possible new primes'
                        for (i = Primes[NextPrime - 1] + 1; i <= Math.Pow(p, 2) - 1; i++)
                        {
                            //Dont go beyond the first window'
                            if (i >= WindowSize)
                                break; // TODO: might not be correct. Was : Exit For
                            if (Numbers[i - FirstValue].UnFactored == i)
                            {
                                //this is a prime less than SQRT(N), so add it to the list.'
                                Primes[NextPrime] = i;
                                NextPrime += 1;
                            }
                        }
                    }

                    PrimeIndex += 1;
                    //move to the next prime'
                }

                //Now Finish & Emit each one'
                for (k = FirstValue; k <= LastValue; k++)
                {
                    //var _with2 = Numbers[k - FirstValue];
                    //Primes larger than Sqrt(N) will not be finished: '
                    if (Numbers[k - FirstValue].UnFactored > 1)
                    {
                        //Not done factoring, must be an large prime factor remaining: '
                        Numbers[k - FirstValue].Phi = Numbers[k - FirstValue].Phi * (Numbers[k - FirstValue].UnFactored - 1);
                        Numbers[k - FirstValue].UnFactored = 1;
                    }

                    //Emit the value pair: (k, Phi(k)) '
                    EmitPhi(k, Numbers[k - FirstValue].Phi);
                }

                //re-Map to the next window '
                IsFirstWindow = false;
                MapWindow(NextFirst);
            } while (FirstValue <= N);
        }

        public void EmitPhi(long k, long Phi)
        {
            //just a placeholder for now, that raises an event to the display form' 
            // periodically for reporting purposes.  Change this to do the actual'
            // emitting.'
            if ((k % ReportInterval) == 0)
            {
                if (EmitTotientPair != null)
                {
                    EmitTotientPair(this, new TotientPairEventArgs() { K = k, Phi = Phi });
                }
            }
        }

        public void MapWindow(long FirstVal)
        {
            //Efficiently reset the window so that we do not have to re-allocate it.'

            //init all of the boundary values'
            FirstValue = FirstVal;
            PrevLast = FirstValue - 1;
            NextFirst = FirstValue + WindowSize;
            LastValue = NextFirst - 1;

            //Initialize the Numbers prime factor arrays'
            long i = 0;
            for (i = 0; i <= WindowSize - 1; i++)
            {
                // var _with3 = Numbers[i];
                Numbers[i].UnFactored = i + FirstValue;
                //initially equal to the number itself'
                Numbers[i].Phi = 1;
                //starts at multiplicative identity(1)'
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Spellchecker", "CRRSP05:A misspelled word has been found", Justification = "It's a name")]
        public long PiMax(long x)
        {
            //estimate of pi(n) == {primes <= (n)} that is never less'
            // than the actual number of primes. (from P. Dusart, 1999)'
            return (long)((x / Math.Log(x)) * (1.0 + 1.2762 / Math.Log(x)));
        }
    }

}