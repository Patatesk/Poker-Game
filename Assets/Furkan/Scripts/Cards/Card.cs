using MoreMountains.Feedbacks;
using PK.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PK.PokerGame
{
    public class Card : MonoBehaviour,IPointerDownHandler
    {
        public Transform spawnPoint;
        public CardType cardType;
        public int cardValue;
        private Mediator mediator;
        private bool useForUI;
        public MMF_Player discardFeedback;
        public bool canChoose;
        public bool forUI
        {
            get
            {
                return useForUI;
            }
            set
            {
                useForUI = value;
            }
        }
        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
        }
        public void Discard()
        {
            DiscardCardSignal.Trigger(this);
            transform.SetParent(transform.root);
        }
        private void OnEnable()
        {
            ChooseModeSignal.ChooseMode += ChangeChooseMode;
        }
        private void OnDisable()
        {
            ChooseModeSignal.ChooseMode -= ChangeChooseMode;
        }
        private void ChangeChooseMode(bool mode)
        {
            canChoose= mode;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canChoose) return;
            AddExtraCardToHandOrDiscard addExtraCardToHandOrDiscard = new AddExtraCardToHandOrDiscard();
            addExtraCardToHandOrDiscard.parent = transform.parent;
            addExtraCardToHandOrDiscard.discardFeedback = discardFeedback;
            addExtraCardToHandOrDiscard.whatHappens = WhatHappensExtraCard.AddToHand;
            addExtraCardToHandOrDiscard.siblingIndex = transform.GetSiblingIndex();
            mediator.Publish(addExtraCardToHandOrDiscard);

        }

        private void OnTriggerEnter(Collider other)
        {
            GetBackQueueSignal.Trigger(this);
            this.gameObject.SetActive(false);
        }
    }
}
