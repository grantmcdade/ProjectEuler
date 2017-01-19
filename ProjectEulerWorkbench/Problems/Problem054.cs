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
    class Problem054 : IProblem
    {
        private IPathProvider _provider;

        public Problem054(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "How many hands did player one win in the game of poker?"; }
        }

        public string Solve()
        {
            string[] rawRounds = File.ReadAllLines(_provider.GetFullyQualifiedPath("poker.txt"));

            var rounds = new List<Round>();
            foreach (var round in rawRounds)
            {
                string[] cards = round.Split(' ');
                var player1 = new Hand();
                for (int i = 0; i < 5; i++)
                {
                    player1.Cards[i] = new Card(cards[i]);
                }
                var player2 = new Hand();
                for (int i = 5; i < 10; i++)
                {
                    player2.Cards[i - 5] = new Card(cards[i]);
                }
                rounds.Add(new Round(player1, player2));
            }

            int wins = 0;
            foreach (var round in rounds)
            {
                if (round.Item1 > round.Item2)
                    ++wins;
            }

            return Convert.ToString(wins);
        }

        private enum HandRank
        {
            HighCard, // Highest value card.
            OnePair, // Two cards of the same value.
            TwoPairs, // Two different pairs.
            ThreeofaKind, // Three cards of the same value.
            Straight, // All cards are consecutive values.
            Flush, // All cards of the same suit.
            FullHouse, // Three of a kind and a pair.
            FourofaKind, // Four cards of the same value.
            StraightFlush, // All cards are consecutive values of same suit.
            RoyalFlush, // Ten, Jack, Queen, King, Ace, in same suit.
        }

        private class Card : IComparable
        {
            public string Code { get; private set; }

            public Card(string code)
            {
                Code = code;
            }

            public string Suit
            {
                get
                {
                    return Code.Substring(1);
                }

            }

            public int Value
            {
                get
                {
                    switch (Code[0])
                    {
                        case 'A':
                            return 14;
                        case 'K':
                            return 13;
                        case 'Q':
                            return 12;
                        case 'J':
                            return 11;
                        case 'T':
                            return 10;
                        default:
                            return Code[0] - 48;
                    }
                }

            }

            public int CompareTo(Object obj)
            {
                return Value.CompareTo(((Card)obj).Value);
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }
        }

        private class Hand
        {
            public Card[] Cards { get; private set; }
            public int HandValue { get; set; }

            public Hand()
            {
                Cards = new Card[5];
            }

            public HandRank Rank()
            {
                HandValue = 0;
                HandRank result = HandRank.HighCard;

                // Sort the cards
                Array.Sort(Cards);

                // Check if all cards have the same suit
                int sumOfCardValues = 0;
                bool sameSuit = true;
                string suit = Cards[0].Suit;
                for (int i = 1; i < Cards.Length; i++)
                {
                    sumOfCardValues += Cards[i].Value;
                    if (suit != Cards[i].Suit)
                        sameSuit = false;
                }

                // Check if there is a posibility for a straight starting with an ace
                int matchesForStraight = Cards.Length;
                if (Cards[0].Value == 2 && Cards[4].Value == 14)
                    matchesForStraight = 4;
                // Check for a straight
                bool isStraight = true;
                for (int i = 1; i < matchesForStraight; i++)
                {
                    if (Cards[i].Value != (Cards[i - 1].Value + 1))
                    {
                        isStraight = false;
                        break;
                    }
                }

                if (sameSuit)
                {
                    // Can be royal flush, straight flush, or flush
                    bool isRoyalFlush = true;
                    for (int i = 0; i < Cards.Length; i++)
                    {
                        if (Cards[i].Value != (10 + i))
                        {
                            isRoyalFlush = false;
                            break;
                        }
                    }
                    if (isRoyalFlush)
                        return HandRank.RoyalFlush;

                    if (isStraight)
                    {
                        return HandRank.StraightFlush;
                    }

                    result = HandRank.Flush;
                }

                var cardGroups = new Dictionary<int, int>();
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (!cardGroups.ContainsKey(Cards[i].Value))
                        cardGroups.Add(Cards[i].Value, 1);
                    else
                        cardGroups[Cards[i].Value] += 1;
                }

                // Check for two of a kind
                // Check for tree of a kind
                // Check for four of a kind
                bool isThreeofaKind = false;
                bool isFourofaKind = false;
                int pairs = 0;
                foreach (var group in cardGroups)
                {
                    if (group.Value == 2)
                    {
                        if (group.Key > HandValue && !isThreeofaKind)
                            HandValue = group.Key;
                        ++pairs;
                    }
                    if (group.Value == 3)
                    {
                        HandValue = group.Key;
                        isThreeofaKind = true;
                    }
                    if (group.Value == 4)
                    {
                        HandValue = group.Key;
                        isFourofaKind = true;
                    }
                }

                if (isFourofaKind)
                    return HandRank.FourofaKind;

                // Check for full house
                if (isThreeofaKind && pairs > 0)
                    return HandRank.FullHouse;

                if (result == HandRank.Flush)
                {
                    HandValue = 0;
                    return result;
                }

                if (isStraight)
                {
                    HandValue = 0;
                    return HandRank.Straight;
                }

                if (isThreeofaKind)
                    return HandRank.ThreeofaKind;

                // Check for two pairs
                if (pairs == 2)
                    return HandRank.TwoPairs;

                if (pairs == 1)
                    return HandRank.OnePair;

                HandValue = Cards[4].Value;
                return HandRank.HighCard;
            }

            public static bool operator >(Hand player1, Hand player2)
            {
                HandRank player1Rank = player1.Rank();
                HandRank player2Rank = player2.Rank();
                if (player1Rank == player2Rank)
                {
                    if (player1.HandValue == player2.HandValue)
                    {
                        for (int i = 4; i >= 0; i--)
                        {
                            if (player1.Cards[i].Value != player2.Cards[i].Value)
                                return player1.Cards[i].Value > player2.Cards[i].Value;
                        }
                        throw new InvalidOperationException("Tie found, this should not happen!");
                    }
                    return player1.HandValue > player2.HandValue;
                }
                return player1Rank > player2Rank;
            }

            public static bool operator <(Hand player1, Hand player2)
            {
                // This is ok because player1 == player2 will throw an exception
                // it's not a valid case
                return !(player1 > player2);
            }
        }

        private class Round : Tuple<Hand, Hand>
        {
            public Round(Hand player1, Hand player2)
                : base(player1, player2)
            {
            }
        }
    }
}
