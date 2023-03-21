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
        private void OnEnable()
        {
            DiscardCardSignal.Discard += Discard;
        }
        private void OnDisable()
        {
            DiscardCardSignal.Discard -= Discard;
        }

        private  IEnumerator Start()
        {
            yield return new WaitForSeconds(.5f);
            GetRandomTwoCard();
        }
        public void BuildAIHand()
        {
            BuildAIHandSignal.Trigger(hand);
        }
        public void AddCardToHand(Card card)
        {
            if (totalCardCount == 5)
            {
                if (isPlayer)
                    ExtraCardCollectedSginal.Trigger(card, AddCardToHandRankHand);
                return;
            }
            AddCardToHandRankHand(card);
            PublishCard(card);
            totalCardCount++;
        }
        private void AddCardToHandRankHand(Card card)
        {
            hand.Add(card);
            var ranking = handRanker.GetHandRank(hand);
            handRank = ranking.handRank;
            ranksBiggestNumber = ranking.biggestNumber;
        }
        private void PublishCard(Card card)
        {
            if (!isPlayer) return;
            RequestCard request = new RequestCard();
            request.hand = UIHand.GetComponent<HandChildHandler>();
            request.cardValue = card.cardValue;
            request.type = card.cardType;
            request.chooseCardTransform = null;
            mediator.Publish(request);
        }
        private void Discard(Card card)
        {
            if (!isPlayer) return;
            hand.Remove(card); 
        }
        private void GetRandomTwoCard()
        {
            for (int i = 0; i < 2; i++)
            {
                CardType type = (CardType)Random.Range(0, 3);
                int value = Random.Range(2, 14);
                RequestCard request = new RequestCard();
                request.addCardToHand += AddCardToHand;
                request.type = type;
                request.cardValue = value;
                request.hand = null;
                request.chooseCardTransform = null;
                mediator.Publish(request);
            }
        }

    }

    public class DiscardCardSignal
    {
        public static event System.Action<Card> Discard;
        public static void Trigger(Card card)
        {
            Discard?.Invoke(card);
        }
    }
    
}
