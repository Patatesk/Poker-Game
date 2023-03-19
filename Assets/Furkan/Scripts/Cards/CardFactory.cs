using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PK.Tools;

namespace PK.PokerGame
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private GameObject[] SpadeCardPrefabs;
        [SerializeField] private GameObject[] DiamondCardPrefabs;
        [SerializeField] private GameObject[] ClubCardPrefabs;
        [SerializeField] private GameObject[] HeartCardPrefabs;

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
            GameObject card = Instantiate(FindCard(request.type, request.cardValue));
            Card script= card.GetComponent<Card>();
            script.forUI = true;
            if (request.hand != null)
            {
                card.transform.SetParent(request.hand.transform.GetChild(0));
                card.transform.SetSiblingIndex(request.hand.ReturnEmptySpace());
                card.SetActive(true);
            }
            else if(request.chooseCardTransform != null)
            {
                card.transform.SetParent(request.chooseCardTransform);
                card.SetActive(true);
            }

        }


        private GameObject FindCard(CardType type, int cardValue)
        {
            GameObject card = null;
            switch (type)
            {
                case CardType.Spade:
                    card = SpadeCardPrefabs[cardValue-1];
                    break;
                case CardType.Diamond:
                    card= DiamondCardPrefabs[cardValue-1];
                    break;
                case CardType.Clube:
                    card= ClubCardPrefabs[cardValue-1];
                    break;
                case CardType.Heart:
                    card= HeartCardPrefabs[cardValue-1];
                    break;
            }
            return card;
        }
    }

    public class RequestCard : ICommand
    {
        public CardType type;
        public int cardValue;
        public HandChildHandler hand;
        public Transform chooseCardTransform;
    }
}
