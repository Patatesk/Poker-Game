using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class HandManager : MonoBehaviour
    {
        public int totalCardCount;
        private List<Card> hand = new List<Card>();
        private HandRanker handRanker;

        private int handRank;
        private int ranksBiggestNumber;

        private void Awake()
        {
            handRanker = new HandRanker();
        }

        public void AddCardToHand(Card card)
        {
            hand.Add(card);
            var ranking = handRanker.GetHandRank(hand);
            handRank = ranking.handRank;
            ranksBiggestNumber = ranking.biggestNumber;
            totalCardCount++;
        }
    }
}
