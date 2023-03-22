using PK.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class ChangeHandler : MonoBehaviour
    {
        private Card toDiscard;
        private Card toAdd;
        private Mediator mediator;
        private Player player;
        private FightManager fightManager;

        private Transform parent;
        private int siblinIndex;
        private void Awake()
        {
             mediator = GameObject.FindAnyObjectByType<Mediator>();
            player = GameObject.FindObjectOfType<Player>();
            fightManager = GameObject.FindObjectOfType<FightManager>();
        }




        private void OnEnable()
        {
            mediator.Subscribe<ChangeCardDelivery>(AddCard);
        }

        private void OnDisable()
        {
            mediator.DeleteSubscriber<ChangeCardDelivery>(AddCard);
        }
        [ContextMenu("Deneme")]
        public void CahngeCards()
        {
            CanSelectableSignal.Trigger(false);
            toDiscard.discardFeedback.PlayFeedbacks();
            toAdd.transform.SetParent(parent);
            toAdd.transform.SetSiblingIndex(siblinIndex);
            player.AddCard(toAdd);
            fightManager.CardsChanged();

        }

        private void AddCard(ChangeCardDelivery delivery)
        {
            if (delivery.isOwnedByPlayer)
            {
                toDiscard = delivery.card;
                parent = delivery.parent;
                siblinIndex = delivery.siblingIndex;
            }
            else
            {
                toAdd = delivery.card;
            }
        }
    }

    public class ChangeCardDelivery:ICommand
    {
        public bool isOwnedByPlayer;
        public Card card;
        public Transform parent;
        public int siblingIndex;
    }
}
