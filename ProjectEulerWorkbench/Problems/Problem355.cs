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
using System.Numerics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem355 : IProblem
    {
        public string Description
        {
            get { return "Maximal coprime subset"; }
        }

        public string Solve()
        {
            return Convert.ToString(CoGreedy(30));
        }

        #region Depth First Search, All Permutations
        // This checks all permutations and is VERY slow! Not a workable solution
        // even with dynamic programming to speed up the algorithm it's still way
        // too slow
        private int CoDfsStart(int limit)
        {
            var cache = new Cache();
            var candidates = new BitArray(limit + 1, true);
            var maximalSum = 0;
            for (int i = 2; i < limit; i++)
            {
                //System.Diagnostics.Debug.WriteLine("CoDfsStart");
                candidates.SetAll(true);
                var sum = CoDfs(candidates, cache, i);
                if (sum > maximalSum)
                    maximalSum = sum;
                //System.Diagnostics.Debug.WriteLine("CoDfsEnd - {0}", sum);
            }
            return maximalSum + 1;
        }

        private int CoDfs(BitArray candidates, Cache cache, int number)
        {
            var maximalSum = 0;
            CancelNumber(candidates, number);

            var id = new Key(candidates.ToBigInteger(), number);
            if (cache.ContainsKey(id))
                return cache[id].Item1;

            var maximalCandidate = 0;
            for (int i = number + 1; i < candidates.Length; i++)
            {
                if (candidates[i])
                {
                    var sum = CoDfs(new BitArray(candidates), cache, i);
                    if (sum > maximalSum)
                    {
                        maximalSum = sum;
                        maximalCandidate = i;
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine("{0} => {1}", number, maximalCandidate);

            var result = maximalSum + number;
            cache.Add(id, new Value(result, maximalCandidate));
            return result;
        }

        private class Key : Tuple<BigInteger, int>
        {
            public Key(BigInteger item1, int item2)
                : base(item1, item2)
            {
            }
        }

        private class Value : Tuple<int, int>
        {
            public Value(int item1, int item2)
                : base(item1, item2)
            {
            }
        }

        private class Cache : Dictionary<Key, Value>
        {
        }

        #endregion

        #region Greedy Algorithm
        private int CoGreedy(int limit)
        {
            var currentSet = new List<int>();
            var candidates = new HashSet<int>();
            for (int i = 2; i <= limit; i++)
                candidates.Add(i);

            // _start = 2;
            // _rstart = limit;

            // Initialise the prime checker
            PreparePrimeSieve(limit);

            // Initialise the totient values
            // CalculateTotientValues(limit);

            // Remove multiples of primes as candidates, they would cancel too many numbers
            var product = 1;
            for (int i = 2; i < limit; i++)
            {
                if (_primes[i])
                {
                    product = product * i;
                    if (!candidates.Contains(product))
                        break;
                    candidates.Remove(product);
                }
            }

            // Declare a cache to use for cleaning out numbers which are no longer coprime options
            // int[] numbersToRemove = new int[candidates.Count];

            do
            {
                // Select a candidate
                var candidate = SelectCandidate(candidates, limit);

                // Runtime ~315ms for Co(10000)
                #region Remove using a cache
                //var cacheIndex = 0;
                //foreach (var item in candidates)
                //    if (Util.Gcd(candidate, item) > 1)
                //        numbersToRemove[cacheIndex++] = item;
                //for (int i = 0; i < cacheIndex; i++)
                //    candidates.Remove(numbersToRemove[i]);
                #endregion

                // Runtime around 3ms for Co(10000)
                #region Remove using all factors
                for (int i = candidate; i <= limit; i += candidate)
                    candidates.Remove(i);
                var sq = (int)Math.Sqrt(candidate);
                for (int i = 2; i <= sq; i++)
                    if (candidate % i == 0)
                    {
                        for (int j = i; j <= limit; j += i)
                            candidates.Remove(j);

                        var k = candidate / i;
                        for (int j = k; j <= limit; j += k)
                            candidates.Remove(k);
                    }
                #endregion

                // Add the candidate to the result set
                currentSet.Add(candidate);

            } while (candidates.Count > 0);

            var sum = currentSet.Sum() + 1;
            return sum;
        }

        // private int _start;
        // private int _rstart;
        private int SelectCandidate(HashSet<int> candidates, int limit)
        {
            // TODO: The key logic for the greedy algorithm goes here, selecting
            // the best next element is key in finding the optimal solution to the
            // problem
            var result = 0;

            #region Odd Numbers
            // Select the largest odd number followed by the highest remaining
            // number if no odd numbers are left

            //foreach (var candidate in candidates)
            //    if (candidate % 2 != 0 && candidate > result)
            //        result = candidate;

            //if (result == 0)
            //    result = candidates.Last();
            #endregion

            #region Prime Numbers
            // Select the highest multiple of an odd prime number as the next candidate
            // followed by the highest remaining number if no primes are available
            //for (int i = limit - 1; i >= 0; i -= 2)
            //    if (_primes[i] && candidates.Contains(i))
            //    {
            //        for (int j = i; j <= limit; j += i)
            //            if (candidates.Contains(j))
            //                result = j;
            //        break;
            //    }

            //if (result == 0)
            //    result = candidates.Last();

            //// Sanity check!
            //System.Diagnostics.Debug.Assert(candidates.Contains(result));
            #endregion

            #region Largest Available Number
            result = candidates.Last();
            //for (int i = _rstart; i >= 0; i--)
            //    if (candidates.Contains(i))
            //    {
            //        result = i;
            //        _rstart = i;
            //        break;
            //    }
            #endregion

            #region Smallest Available Number
            //for (int i = _start; i <= limit; i++)
            //    if (candidates.Contains(i))
            //    {
            //        result = i;
            //        _start = i;
            //        break;
            //    }
            #endregion

            #region Next number with totient ratio <= 2
            //foreach (var item in candidates.Reverse())
            //{
            //    if ((double)item / (double)_totients[item] <= 2.0)
            //    {
            //        result = item;
            //        break;
            //    }
            //}
            #endregion

            #region Next number with the highest totient
            //var highestTotient = 0L;
            //foreach (var item in candidates)
            //{
            //    if (_totients[item] >= highestTotient)
            //    {
            //        highestTotient = _totients[item];
            //        result = item;
            //    }
            //}
            #endregion

            return result;
        }
        #endregion
        
        #region Failed Attempts
        
        #region Playing with set ideas
        private int CoSets(int limit)
        {
            var sets = new Tuple<int, int>[limit / 2];

            var setIndex = 1;
            sets[0] = new Tuple<int, int>(2, limit - (limit % 2));
            for (int i = 1; i < limit; i += 2)
            {
                //var item2 = 0;
                //while (item2 + i <= limit)
                //{
                //    item2 += i;
                //}
                //sets[setIndex] = new Tuple<int, int>(i, item2);
                sets[setIndex] = new Tuple<int, int>(i, limit - (limit % i));
                ++setIndex;
            }

            return 0;
        }
        #endregion

        #region Create subsets using factor based weighting
        private int CoPrimes(int limit)
        {
            bool[] primes = new bool[limit + 1];
            primes[2] = true;
            for (int i = 3; i <= limit; i += 2)
                primes[i] = true;
            var sq = (int)Math.Sqrt(limit);
            for (int i = 3; i <= sq; i++)
                if (primes[i])
                    for (int j = i + i; j <= limit; j += i)
                        primes[j] = false;


            // Count the factors of every available number
            var maxFactors = 0;
            var factors = new int[limit + 1];
            for (int i = 1; i <= limit; i++)
            {
                if (primes[i])
                {
                    for (int j = i + i; j <= limit; j += i)
                    {
                        factors[j] += 1;
                        if (factors[j] > maxFactors)
                            maxFactors = factors[j];
                    }
                }
            }

            // Only coposites with less than two ways of being produced should be added to the maximal set
            var maximalSum = 0;
            var maximalSet = new List<int>();
            var candidates = new BitArray(limit + 1, true);
            for (int factorLevel = maxFactors; factorLevel >= 0; factorLevel--)
            {
                candidates.SetAll(true);
                maximalSet.Clear();
                for (int i = limit; i > 1; i--)
                {
                    var currentFactorLevel = factorLevel;
                    if (candidates[i] && (factors[i] <= factorLevel))
                    {
                        maximalSet.Add(i);
                        CancelNumber(candidates, i);
                    }
                }
                maximalSet.Add(1);

                foreach (var item in maximalSet)
                    System.Diagnostics.Debug.Write(item + ", ");
                System.Diagnostics.Debug.WriteLine(String.Empty);

                // Try to optimise the set
                //while (optimiseBySplitting(maximalSet, limit)) { }
                //while (optimiseUsingLargerCommonFactor(maximalSet, limit)) { }

                var sum = maximalSet.Sum();
                if (sum > maximalSum)
                    maximalSum = sum;
            }

            return maximalSum;
        }

        public bool optimiseBySplitting(List<int> maximalSet, int limit)
        {
            var replacementFound = false;
            // For each value in the set check if we can replace it with two other mutually coprime
            // values which produce a larger sum
            for (int i = 0; i < maximalSet.Count; i++)
            {
                var workingValue = maximalSet[i];
                var workingSet = new List<int>(maximalSet);
                workingSet.RemoveAt(i);

                // Prepare a list of candidate replacements for the current number
                var candidates = new BitArray(limit + 1, true);
                for (int j = 0; j < workingSet.Count; j++)
                {
                    CancelNumber(candidates, workingSet[j]);
                }

                // Select two coprime numbers from the list of candidates and check if they
                // produce a larger sum than the number we are removing
                for (int j = 2; j <= limit; j++)
                {
                    if (candidates[j])
                    {
                        for (int k = j + 1; k <= limit; k++)
                        {
                            if (candidates[k] && k % j != 0)
                            {
                                if (k + j > workingValue)
                                {
                                    // Replacement found!
                                    maximalSet.RemoveAt(i);
                                    maximalSet.Add(j);
                                    maximalSet.Add(k);
                                    replacementFound = true;
                                    break;
                                }
                            }
                        }
                        if (replacementFound)
                            break;
                    }
                }
                if (replacementFound)
                    break;
            }
            return replacementFound;
        }

        public bool optimiseUsingLargerCommonFactor(List<int> maximalSet, int limit)
        {
            // For each pair of numbers in the set find the largest common factor smaller than
            // the limit and check if the two numbers can be replaced by the larger one
            var candidates = new BitArray(limit + 1, true);
            for (int i = 0; i < maximalSet.Count; i++)
            {
                for (int j = i; j < maximalSet.Count; j++)
                {
                    // Let a be the small number and b be the large number
                    var a = maximalSet[i] < maximalSet[j] ? maximalSet[i] : maximalSet[j];
                    var b = maximalSet[i] > maximalSet[j] ? maximalSet[i] : maximalSet[j];

                    // Don't scan for a == b since we can't replace both numbers
                    if (a == b)
                        continue;

                    // Don't try to replace 1, it's always valid in every set
                    if (a == 1)
                        continue;

                    // Cancel all multiples of a
                    candidates.SetAll(true);
                    CancelNumber(candidates, a);

                    // Find any multiples of b larger than b which are also multiples of a (already cancelled)
                    for (int f = 2; f < a; f++)
                    {
                        if (b % f == 0)
                        {
                            for (int k = f; k < limit; k += f)
                            {
                                if (!candidates[k])
                                {
                                    // Found! The value k is a larger multiple of both a and b, if it is also larger than the
                                    // sum of a and b then it's a better option to use in the set
                                    var sum = a + b;
                                    if (k > sum)
                                    {
                                        // Replace a and b with k
                                        maximalSet.RemoveAt(j);
                                        maximalSet.RemoveAt(i);
                                        maximalSet.Add(k);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }
        #endregion

        #region Attempt to uses weights
        private int CoWeights2(int limit)
        {
            int[] weights = new int[limit + 1];
            var candidates = new BitArray(limit + 1, true);
            for (int n = 2; n <= limit; n++)
            {
                candidates.SetAll(true);
                weights[n] = n;
                CancelNumber(candidates, n);
                for (int c = 2; c <= limit; c++)
                {
                    if (candidates.Get(c))
                    {
                        weights[n] += c;
                        CancelNumber(candidates, c);
                    }
                }
            }

            candidates.SetAll(true);
            var currentSet = new List<int>();
            currentSet.Add(1);
            for (int c = 2; c <= limit; c++)
            {
                // Find the most valuable candidate which can still be added to the set
                var maxCandidate = 0;
                var maxWeight = 0;
                for (int m = 2; m <= limit; m++)
                {
                    if (candidates.Get(m) && weights[m] >= maxWeight)
                    {
                        maxWeight = weights[m];
                        maxCandidate = m;
                    }
                }
                if (maxCandidate == 0)
                    break;
                currentSet.Add(maxCandidate);
                CancelNumber(candidates, maxCandidate);
            }

            return currentSet.Sum();
        }

        private int CoWeights(int limit)
        {
            int[] weights = new int[limit + 1];
            for (int j = 2; j <= limit; j++)
            {
                var candidates = new BitArray(limit + 1, true);
                var sq = (int)Math.Sqrt(j);
                for (int k = 2; k <= sq; k++)
                {
                    if (j % k == 0)
                    {
                        // k is a factor of j
                        for (int l = k; l <= limit; l += k)
                        {
                            if (candidates.Get(l))
                            {
                                weights[j] += l;
                                candidates.Set(l, false);
                            }
                        }

                        var upperFactor = j / k;
                        for (int l = upperFactor; l <= limit; l += upperFactor)
                        {
                            if (candidates.Get(l))
                            {
                                weights[j] += l;
                                candidates.Set(l, false);
                            }
                        }
                    }
                }
                for (int l = j; l <= limit; l += j)
                {
                    if (candidates.Get(l))
                    {
                        weights[j] += l;
                        candidates.Set(l, false);
                    }
                }
            }
            return 0;
        }
        #endregion

        #region Attempted Recursive Approach
        private ulong CoRecursiveStart(ulong limit)
        {
            bool[] candidates = new bool[limit + 1];
            // Reset the candidate list
            for (int i = 0; i < candidates.Length; i++)
                candidates[i] = true;

            return CoRecursive(candidates, 2, limit);
        }

        private ulong CoRecursive(bool[] candidates, ulong start, ulong limit)
        {
            var maximumSum = 0UL;
            List<ulong> maximalSet = null;
            for (ulong n = 2; n <= limit; n++)
            {
                // We include 1 in every set since it's always a valid member and therefore the
                // count starts at 1 and we don't check that value
                var currentSum = 1UL;
                var currentSet = new List<ulong>();
                currentSet.Add(1UL);
                for (ulong i = n; i <= limit; i++)
                {
                    if (candidates[i])
                    {
                        var currentCandidate = i;
                        var newCandidate = 0UL;
                        bool[] eliminationForNewCandidate;
                        do
                        {
                            if (newCandidate > currentCandidate)
                                currentCandidate = newCandidate;
                            eliminationForNewCandidate = (bool[])candidates.Clone();
                            newCandidate = EliminateCoprimePairs(eliminationForNewCandidate, currentCandidate);
                        } while (newCandidate != currentCandidate);

                        for (int j = 0; j < candidates.Length; j++)
                            candidates[j] = eliminationForNewCandidate[j];

                        //// Cancel out all coprime pairs of the candidate i
                        //var largestCandidate = EliminateCoprimePairs(candidates, i);
                        //if (largestCandidate != 0 && largestCandidate != i)
                        //{
                        //    // Cancel out all coprime pairs for the new candidate
                        //    var newLargestCandidate = EliminateCoprimePairs(candidates, largestCandidate);
                        //    if (newLargestCandidate > largestCandidate)
                        //    {
                        //        var hello = 0;
                        //    }
                        //}
                        currentSum += newCandidate;
                        currentSet.Add(newCandidate);
                    }
                }
                if (currentSum > maximumSum)
                {
                    maximumSum = currentSum;
                    maximalSet = new List<ulong>(currentSet);
                }

                // Reset the candidate list
                for (int i = 0; i < candidates.Length; i++)
                    candidates[i] = true;

                // Clear the set
                currentSet.Clear();
            }

            // Verify that the maximum set consists only of mutually coprime numbers
            for (int i = 0; i < maximalSet.Count; i++)
            {
                for (int j = i + 1; j < maximalSet.Count; j++)
                {
                    if (!Util.Coprime(maximalSet[i], maximalSet[j]))
                    {
                        throw new Exception("Maximal set is not mutually coprime!");
                    }
                }
            }

            return maximumSum;
        }

        private ulong EliminateCoprimePairs(bool[] candidates, ulong coprime)
        {
            // Cancel out all factors larger than 1 in the current candidate
            var largestCandidate = 0UL;
            var limit = (ulong)candidates.Length;
            var sq = (ulong)Math.Sqrt(coprime);
            for (ulong j = 1; j <= sq; j++)
            {
                if (coprime % j == 0)
                {
                    var lowDivisor = j;
                    if (j == 1)
                        lowDivisor = coprime;

                    for (ulong k = lowDivisor; k < limit; k += lowDivisor)
                    {
                        // Find the largest multiple which is still a candidate and add
                        // that one to the sum instead
                        if (candidates[k] && k > largestCandidate)
                            largestCandidate = k;
                        candidates[k] = false;
                    }

                    if (lowDivisor != coprime)
                    {
                        var highDivisor = coprime / lowDivisor;
                        for (ulong k = highDivisor; k < limit; k += highDivisor)
                        {
                            // Find the largest multiple which is still a candidate and add
                            // that one to the sum instead
                            if (candidates[k] && k > largestCandidate)
                                largestCandidate = k;
                            candidates[k] = false;
                        }
                    }
                }
            }

            return largestCandidate;
        }
        #endregion

        #region Binary Permutation Generation
        // This works but is unacceptably slow
        private ulong CoAllPermutations(int limit)
        {
            ulong result = 0;
            ulong subsetLimit = Util.Pow((ulong)2, limit) - 1;
            var subset = new List<ulong>();
            int[,] coprimePairs = new int[limit + 1, limit + 1];
            for (ulong i = 0; i < subsetLimit; i++)
            {
                // Generate the set to represent this binary count
                subset.Clear();
                for (int j = 1; j <= limit; j++)
                {
                    ulong mask = (ulong)1 << (j - 1);
                    if ((i & mask) == mask)
                    {
                        subset.Add((ulong)j);
                    }
                }

                // Check if all the elemnts in this set are co-prime
                bool coprimeSet = true;
                for (int j = 0; j < subset.Count(); j++)
                {
                    for (int k = j + 1; k < subset.Count(); k++)
                    {
                        if (j != k)
                        {
                            if (coprimePairs[subset[j], subset[k]] == 0)
                                if (Util.Coprime(subset[j], subset[k]))
                                    coprimePairs[subset[j], subset[k]] = 1;
                                else
                                    coprimePairs[subset[j], subset[k]] = 2;

                            if (coprimePairs[subset[j], subset[k]] != 2)
                            {
                                coprimeSet = false;
                                break;
                            }
                        }
                    }
                    if (!coprimeSet)
                        break;
                }

                if (coprimeSet)
                {
                    // Sum all the components, if this is the larest sum so far
                    // then keep it as our result
                    ulong sum = 0;
                    for (int j = 0; j < subset.Count(); j++)
                    {
                        sum += subset[j];
                    }
                    if (sum > result)
                        result = sum;
                }
            }
            return result;
        }
        #endregion

        #endregion

        #region Utility Functions
        private BitArray _primes;
        private void PreparePrimeSieve(int limit)
        {
            _primes = new BitArray(limit + 1, false);
            _primes[2] = true;
            for (int i = 3; i <= limit; i += 2)
                _primes[i] = true;
            var sq = (int)Math.Sqrt(limit);
            for (int i = 3; i <= sq; i++)
                if (_primes[i])
                    for (int j = i + i; j <= limit; j += i)
                        _primes[j] = false;
        }

        private long[] _totients;
        private void CalculateTotientValues(int limit)
        {
            _totients = new long[limit + 1];

            // Naive approach, simply count the number of lesser coprime values
            // for each number in the list, this is not efficient for large arrays
            //for (int i = 1; i <= limit; i++)
            //    for (int j = 1; j < i; j++)
            //        if (Util.Gcd(i, j) == 1)
            //            ++_totients[i];

            //var totientCalculator = new TotientSerialCalculator();
            //totientCalculator.EmitTotientPair += new TotientSerialCalculator.EmitTotientPairEventHandler(totientCalculator_EmitTotientPair);
            //totientCalculator.EmitTotientPairsToN(limit + 1);

            for (int i = 1; i <= limit; i++)
            {
                var max = i;
                var totient = i;
                for (int p = 2; p <= max; p++)
                    if (i % p == 0 && _primes[p])
                        totient -= totient / p;
                _totients[i] = totient;
            }

            //for (int i = 1; i <= limit; i++)
            //    _totients[i] = 1;
            //for (int i = 2; i <= limit; i++)
            //{
            //    if (Util.IsPrime(i))
            //    {
            //        for (int j = i; j <= limit; j += i)
            //        {
            //            var start = j + 1;
            //            var end = j + i;
            //            end = end < limit ? end : limit;
            //            for (int k = start; k < end; k++)
            //                ++_totients[k];
            //        }
            //    }
            //}
        }

        //void totientCalculator_EmitTotientPair(long k, long Phi)
        //{
        //    _totients[k] = Phi;
        //}

        private void CancelNumber(BitArray candidates, int number)
        {
            // Cancel out all multiples of this number and it's factors
            var sq = (int)Math.Sqrt(number);
            for (int i = 2; i <= sq; i++)
            {
                if (number % i == 0)
                {
                    CancelMultiples(candidates, i);
                    CancelMultiples(candidates, number / i);
                }
            }
            CancelMultiples(candidates, number);
        }

        private void CancelMultiples(BitArray candidates, int value)
        {
            for (int m = value; m < candidates.Length; m += value)
                candidates.Set(m, false);
        }
        #endregion
    }
}
