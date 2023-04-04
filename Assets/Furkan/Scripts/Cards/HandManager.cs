using DG.Tweening;
using PK.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace PK.PokerGame
{
    public class HandManager : MonoBehaviour
    {
        public int totalCardCount;

        [SerializeField] private bool isPlayer;
        [SerializeField] private GameObject UIHand;

        private List<Card> hand = new List<Card>();
        private Dictionary<int, int> values = new Dictionary<int, int>();
        private Dictionary<CardType, int> suits = new Dictionary<CardType, int>();
        private HandRanker handRanker;
        private Mediator mediator;
        private Player player;
        private AI AI;
        private PlayerStarHandler playerStarHandler;

        public int handRank;
        public int ranksBiggestNumber;

        private void Awake()
        {
            handRanker = new HandRanker();
            mediator = GameObject.FindObjectOfType<Mediator>();
            player = GetComponent<Player>();
            AI = GetComponent<AI>();
            playerStarHandler = GetComponent<PlayerStarHandler>();
        }

        private void OnEnable()
        {
            DiscardCardSignal.Discard += Discard;
            GameStartSignal.gameStart += RequestHand;
        }
        private void OnDisable()
        {
            DiscardCardSignal.Discard -= Discard;
            GameStartSignal.gameStart -= RequestHand;
        }

        private void RequestHand()
        {
            StartCoroutine(GetHands());
        }

        private IEnumerator GetHands()
        {
            yield return new WaitForSeconds(.2f);
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
                {
                    ExtraCardCollectedSginal.Trigger(card, AddCardToHandRankHand);
                    return;
                }
                else if (!isPlayer && handRank < 5)
                {
                    Card canPair = CheckCanBePair(card);
                    if (canPair)
                    {
                        int forRemoveValue = CheckForLonelyValues(canPair);
                        if (forRemoveValue == -1) return;
                        AýDiscard(FindCard(forRemoveValue));
                    }
                    else return;
                }
                else if (handRank >= 5) return;
            }
            AddCardToHandRankHand(card);
            PublishCard(card);
            if (isPlayer)
            {
                player.ScaleUp(totalCardCount);
            }
            else
            {
                AI.ScaleUp(totalCardCount);
            }
        }
        public void AddCardToHandRankHand(Card card)
        {
            hand.Add(card);
            AddDictioneryOrRemove(true, card);
            totalCardCount++;
            var ranking = handRanker.GetHandRank(hand);
            handRank = ranking.handRank;
            ranksBiggestNumber = ranking.biggestNumber;
            if (!isPlayer) return;
            if (handRank == 1 || handRank == 2 || handRank == 3) playerStarHandler.SetStars(1);
            else if (handRank == 4 || handRank == 5 || handRank == 6) playerStarHandler.SetStars(2);
            else playerStarHandler.SetStars(3);
            ShowHandNameSignal.Trigger(handRank);
        }
        private Card CheckCanBePair(Card card)
        {
            Card canPair = null;
            if (values.ContainsKey(card.cardValue))
            {
                canPair = FindCard(card.cardValue);
            }

            return canPair;
        }
        private int CheckForLonelyValues(Card card)
        {
            int lonelyValue = -1;
            foreach (KeyValuePair<int, int> value in values)
            {
                if (value.Value == card.cardValue) continue;
                if (value.Value == 1)
                {
                    lonelyValue = value.Key;
                    break;
                }
            }
            return lonelyValue;
        }

        private void AddDictioneryOrRemove(bool ShouldAdd, Card _card)
        {
            if (ShouldAdd)
            {
                if (!values.ContainsKey(_card.cardValue)) values.Add(_card.cardValue, 0);
                values[_card.cardValue]++;
                if (!suits.ContainsKey(_card.cardType)) suits.Add(_card.cardType, 0);
                suits[_card.cardType]++;
            }
            else
            {
                if (values.ContainsKey(_card.cardValue))
                    values[_card.cardValue]--;
                if (suits.ContainsKey(_card.cardType))
                    suits[_card.cardType]--;
            }
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

        private Card FindCard(int value, CardType type = CardType.None)
        {
            Card findedCard = null;
            if (type != CardType.None)
            {
                foreach (Card _Card in hand)
                {
                    if (value == _Card.cardValue
                        && type == _Card.cardType)
                    {
                        findedCard = _Card;
                        break;
                    }
                }
            }
            else
            {
                foreach (Card _Card in hand)
                {
                    if (value == _Card.cardValue
                       )
                    {
                        findedCard = _Card;
                        break;
                    }
                }
            }
            return findedCard;
        }
        private void Discard(Card card)
        {

            if (!isPlayer) return;
            if (!card.transform.parent.CompareTag("Deck")) return;
            Card discard = FindCard(card.cardValue, card.cardType);
            hand.Remove(discard);
            totalCardCount--;
        }
        private void AýDiscard(Card card)
        {
            Card discard = FindCard(card.cardValue, card.cardType);
            hand.Remove(discard);
            AddDictioneryOrRemove(false, discard);
            totalCardCount--;
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
                request.forSTartHand = true;
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
