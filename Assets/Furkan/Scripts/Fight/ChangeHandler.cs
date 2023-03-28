using PK.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class ChangeHandler : MonoBehaviour
    {
        [SerializeField] private GameObject buttons;

        private Card toDiscard;
        private Card toAdd;
        private Mediator mediator;
        private Player player;
        private FightManager fightManager;

        private Transform parent;
        private int siblinIndex = 10;
        private HandChildHandler playerHandChildHandler;
        private int totalCards;
        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
            player = GameObject.FindObjectOfType<Player>();
            fightManager = GameObject.FindObjectOfType<FightManager>();
            playerHandChildHandler = GameObject.FindObjectOfType<HandChildHandler>();
        }


       

        private void OnEnable()
        {
            mediator.Subscribe<ChangeCardDelivery>(AddCard);
        }

        private void OnDisable()
        {
            mediator.DeleteSubscriber<ChangeCardDelivery>(AddCard);
        }

        public void CahngeCards()
        {
            if (toAdd == null ) return;
            if (toDiscard == null && player.TotalCardCount() >= 5) return;
            CanSelectableSignal.Trigger(false);
            if(toDiscard != null)  toDiscard.discardFeedback.PlayFeedbacks();
            if (parent == null) parent= playerHandChildHandler.transform.GetChild(0);
            toAdd.transform.SetParent(parent);
            if (siblinIndex == 10) siblinIndex = playerHandChildHandler.ReturnEmptySpace();
            toAdd.transform.SetSiblingIndex(siblinIndex);
            player.AddCard(toAdd);
            fightManager.CardsChanged();
            toAdd.UnSelect();
            buttons.SetActive(false);
            toDiscard = null;
            parent = null;
            siblinIndex = 10;
            toAdd = null;
            CanSelectableSignal.Trigger(false);

        }

        public void Discard()
        {
            toDiscard = null;
            parent = null;
            siblinIndex = 10;
            toAdd= null;
            fightManager.CardsChanged();
            fightManager.ChangeDiscardTime(.2f);
        }
        private void AddCard(ChangeCardDelivery delivery)
        {
            if (!fightManager.isFighting) return;
            buttons.SetActive(true);
            if (delivery.isOwnedByPlayer)
            {
                if (player.TotalCardCount() < 5)
                {
                    return;
                }
                if (toDiscard != null)
                {
                    toDiscard.UnSelect();
                    toDiscard = null;
                }
                toDiscard = delivery.card;
                parent = delivery.parent;
                siblinIndex = delivery.siblingIndex;
                totalCards = delivery.totalCard;
                toDiscard.Select(100);
            }
            else
            {
                if (toAdd != null)
                {
                    toAdd.UnSelect();
                    toAdd = null;
                }
                toAdd = delivery.card;
                toAdd.Select(-100);
            }
        }
    }

    public class ChangeCardDelivery : ICommand
    {
        public bool isOwnedByPlayer;
        public Card card;
        public Transform parent;
        public int siblingIndex;
        public int totalCard;
    }
}
