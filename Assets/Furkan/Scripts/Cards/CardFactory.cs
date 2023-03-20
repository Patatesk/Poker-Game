using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PK.Tools;
using System;

namespace PK.PokerGame
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private GameObject[] SpadeCardPrefabs;
        [SerializeField] private GameObject[] DiamondCardPrefabs;
        [SerializeField] private GameObject[] ClubCardPrefabs;
        [SerializeField] private GameObject[] HeartCardPrefabs;
        [SerializeField] private Transform spawnPoint;
        private Mediator mediator;
        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
        }

        private void OnEnable()
        {
            mediator.Subscribe<RequestCard>(SetHand);
        }
        private void OnDisable()
        {
            mediator.DeleteSubscriber<RequestCard>(SetHand);
        }

        private void SetHand(RequestCard request)
        {
            GameObject card = Instantiate(FindCard(request.type, request.cardValue), spawnPoint);
            Card script = card.GetComponent<Card>();
            script.forUI = true;
            card.SetActive(false);
            if (request.hand != null)
            {
                card.transform.SetParent(request.hand.transform.GetChild(0));
                card.transform.SetSiblingIndex(request.hand.ReturnEmptySpace());
                card.SetActive(true);
            }
            else if (request.chooseCardTransform != null)
            {
                card.transform.SetParent(request.chooseCardTransform);
                card.SetActive(true);
            }
            else if (request.hand == null && request.chooseCardTransform == null)
            {
                request.addCardToHand.Invoke(script);
            }

        }


        private GameObject FindCard(CardType type, int cardValue)
        {
            GameObject card = null;
            switch (type)
            {
                case CardType.Spade:
                    card = SpadeCardPrefabs[cardValue - 2];
                    break;
                case CardType.Diamond:
                    card = DiamondCardPrefabs[cardValue - 2];
                    break;
                case CardType.Clube:
                    card = ClubCardPrefabs[cardValue - 2];
                    break;
                case CardType.Heart:
                    card = HeartCardPrefabs[cardValue - 2];
                    break;
            }
            return card;
        }
    }

    public class RequestCard : ICommand
    {
        public CardType type;
        public int cardValue;
        public HandChildHandler hand = null;
        public Transform chooseCardTransform = null;
        public System.Action<Card> addCardToHand;
    }
}
