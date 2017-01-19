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
using System.IO;
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem059 : IProblem
    {
        private IPathProvider _provider;

        public Problem059(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "Using a brute force attack, can you decrypt the cipher using XOR encryption?"; }
        }

        public string Solve()
        {
            var alphaChars = "abcdefghijklmnopqrstuvwxyz";
            var masterSet = new int[alphaChars.Length];
            Array.Copy(alphaChars.ToCharArray(), masterSet, alphaChars.Length);

            var text = File.ReadAllText(_provider.GetFullyQualifiedPath("cipher1.txt"));
            var asciiCodes = text.Split(',');
            // var key = new int[3];

            var encryptedText = new char[asciiCodes.Length];
            for (int i = 0; i < asciiCodes.Length; i++)
                encryptedText[i] = (char)Byte.Parse(asciiCodes[i]);

            var decryptedText = new char[asciiCodes.Length];
            string decryptedOutput = null;

            //// Use a binary filter to test possible keys, optimise by only
            //// testing three character keys, but be sure to test all
            //// lexicographical permutations of those three keys.
            //int max = (int)Math.Pow(2, alphaChars.Length);
            //for (int i = 0; i < max; i++)
            //{
            //    int index = 0;
            //    for (int j = 0; j < alphaChars.Length; j++)
            //    {
            //        int mask = 1 << j;
            //        if ((i & mask) == mask)
            //        {
            //            if (index > 2)
            //            {
            //                ++index;
            //                break;
            //            }
            //            key[index] = alphaChars[j];
            //            ++index;
            //        }
            //    }
            //    // This is a candidate key only if the three charaters were found for the key
            //    if (index == 3)
            //    {
            // We don't need the sort because the source array is already ordered
            // Array.Sort(key);

            foreach (var key in Sets.GetBinaryPermutations<int>(masterSet, 3))
            {
                foreach (var set in Sets.GetLexicographicPermutations(key))
                {
                    // Stop trying to decrypt, we have no early exit from the lexicographical generator
                    if (decryptedOutput != null)
                        continue;

                    int keyIndex = 0;
                    for (int j = 0; j < encryptedText.Length; j++)
                    {
                        decryptedText[j] = (char)(encryptedText[j] ^ set[keyIndex]);
                        ++keyIndex;
                        if (keyIndex > 2)
                            keyIndex = 0;
                    }

                    // Search for the common English word 'the' in the output
                    bool foundThe = false;
                    bool foundAnd = false;
                    for (int j = 0; j < decryptedText.Length - 5; j++)
                    {
                        if (decryptedText[j] == ' ' && decryptedText[j + 1] == 't' && decryptedText[j + 2] == 'h' && decryptedText[j + 3] == 'e' && decryptedText[j + 4] == ' ')
                            foundThe = true;

                        if (decryptedText[j] == ' ' && decryptedText[j + 1] == 'a' && decryptedText[j + 2] == 'n' && decryptedText[j + 3] == 'd' && decryptedText[j + 4] == ' ')
                            foundAnd = true;

                        if (foundThe && foundAnd)
                        {
                            // Possible scuccessful decryption
                            decryptedOutput = new String(decryptedText);
                            break;
                        }
                    }
                }
                if (decryptedOutput != null)
                    break;
            }

            return Convert.ToString(decryptedOutput.Sum(c => c));
        }
    }
}
