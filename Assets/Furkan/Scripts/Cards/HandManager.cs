using PK.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class HandManager : MonoBehaviour
    {
        public int totalCardCount;

        [SerializeField] private bool isPlayer;
        [SerializeField] private GameObject UIHand;

        private List<Card> hand = new List<Card>();
        private HandRanker handRanker;
        private Mediator mediator;


        private int handRank;
        private int ranksBiggestNumber;

        private void Awake()
        {
            handRanker = new HandRanker();
            mediator = GameObject.FindObjectOfType<Mediator>();
        }
      
        public void AddCardToHand(Card card)
        {
            if (totalCardCount == 5)
            {
                Debug.Log("5 Kart Oldu Seçim Ekranýna Geç");
                return;
            }
            hand.Add(card);
            var ranking = handRanker.GetHandRank(hand);
            handRank = ranking.handRank;
            ranksBiggestNumber = ranking.biggestNumber;
            PublishCard(card);
            totalCardCount++;
        }
        private void PublishCard(Card card)
        {
            RequestCard request = new RequestCard();
            request.hand = UIHand.GetComponent<HandChildHandler>();
            request.cardValue = card.cardValue;
            request.type = card.cardType;
            request.chooseCardTransform = null;
            mediator.Publish(request);
        }

    }
}
