
using PK.Tools;
namespace PK.PokerGame
{
    using System.Collections.Generic;
    using UnityEngine;
    

    public class HandRanker 
    {
        public (int handRank,int biggestNumber) GetHandRank(List<Card> hand)
        {

            Dictionary<int, int> valueCounts = new Dictionary<int, int>();
            Dictionary<CardType, int> suitCounts = new Dictionary<CardType, int>();

            bool flush = false;
            bool straight = false;
            int straightValue = 0;
            int highCardValue = 0;
            int pairValue = 0;
            int threeOfKind = 0;

            // Count the number of occurrences of each value and suit
            for (int i = 0; i < hand.Count; i++)
            {
                int value = hand[i].cardValue;
                CardType suit = hand[i].cardType;

                if (!valueCounts.ContainsKey(value))
                    valueCounts.Add(value, 0);

                valueCounts[value]++;

                if (!suitCounts.ContainsKey(suit))
                    suitCounts.Add(suit, 0);

                suitCounts[suit]++;
            }

            // Check for flush
            foreach (KeyValuePair<CardType, int> suitCount in suitCounts)
            {
                if (suitCount.Value == 5)
                {
                    flush = true;
                    highCardValue = (int)suitCount.Key;
                }
            }

            // Check for straight
            List<int> valueList = new List<int>(valueCounts.Keys);
            
            if (valueList.Count == 5)
            {
                if (valueList.CheckListSortedByValue<int>())
                {
                    straight = true;
                    straightValue = valueList[valueList.Count - 1];
                }
            }


            // Check for straight flush
            if (straight && flush)
                return (9,straightValue); // Straight Flush

            // Check for four of a kind
            foreach (KeyValuePair<int, int> valueCount in valueCounts)
            {
                if (valueCount.Value >= 4)
                    return (8,valueCount.Key); // Four of a Kind
            }

            // Check for full house
            bool threeOfAKind = false;
            bool pair = false;

            foreach (KeyValuePair<int, int> valueCount in valueCounts)
            {
                if (valueCount.Value == 2)
                {
                    pair = true;
                    pairValue =valueCount.Key;
                }

                if (valueCount.Value == 3)
                {
                    threeOfAKind = true;
                    threeOfKind = valueCount.Key;
                }

               
            }

            if (threeOfAKind && pair)
                return (7,threeOfKind); // Full House

            // Check for flush
            if (flush)
                return (6,highCardValue); // Flush

            // Check for straight
            if (straight)
                return (5,straightValue); // Straight

            if (threeOfAKind)
                return (4, threeOfKind);
           

            // Check for two pairs
            int pairs = 0;
            int _pairValue = 0;

            foreach (KeyValuePair<int, int> valueCount in valueCounts)
            {
                if (valueCount.Value == 2)
                {
                    pairs++;
                    if (_pairValue < valueCount.Key)
                    {
                        _pairValue = valueCount.Key;
                    }
                }
                pairValue = _pairValue;
            }

            if (pairs == 2)
                return (3,pairValue); // Two Pairs

            // Check for one pair
            if (pairs == 1)
                return (2,pairValue); // One Pair

            // High Card
            foreach (KeyValuePair<int, int> valueCount in valueCounts)
            {
                if (valueCount.Key > highCardValue)
                    highCardValue = valueCount.Key;
            }

            return (1,highCardValue); // High Card

        }

    }

}
