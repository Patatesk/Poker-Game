using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CardManager : MonoBehaviour
    {
        public int totalCardCount;



        private HandRanker handRanker;

        private void Awake()
        {
            handRanker = new HandRanker();
        }
    }
}
