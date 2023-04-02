using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PK.Tools;
using MoreMountains.Feedbacks;

namespace PK.PokerGame
{
    public class ExtraCardHandler : MonoBehaviour
    {
        private Mediator mediator;
        private Action<Card> addCardToTheHand;
        [SerializeField] private GameObject textHelper;
        [SerializeField] private GameObject discardButton;

        private Card CurrentCard;
        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
        }

        private void OnEnable()
        {
            ExtraCardCollectedSginal.OnCardCollected += RequestCardForThis;
            mediator.Subscribe<AddExtraCardToHandOrDiscard>(AddOrDiscardExtraCard);
        }
        private void OnDisable()
        {
            ExtraCardCollectedSginal.OnCardCollected -= RequestCardForThis;
            mediator.DeleteSubscriber<AddExtraCardToHandOrDiscard>(AddOrDiscardExtraCard);

        }
        private void RequestCardForThis(Card card,Action<Card> _addCardToTheHand)
        {
            if(CurrentCard != null)
            {
                CurrentCard.discardFeedback.PlayFeedbacks();
                CurrentCard = null;
                addCardToTheHand = null;
            }
            RequestCard request = new RequestCard();
            request.cardValue = card.cardValue;
            request.type = card.cardType;
            request.chooseCardTransform = this.transform;
            addCardToTheHand = _addCardToTheHand;
            mediator.Publish(request);
            ChooseModeSignal.Trigger(true);
            textHelper.SetActive(true);
            discardButton.SetActive(true);
        }
        private void OnTransformChildrenChanged()
        {
            if(transform.childCount > 0)
            {
                CurrentCard = transform.GetChild(0).GetComponent<Card>();
                Invoke("PreventCurrentCardChoosing", 0.5f);
            }
        }
        private void PreventCurrentCardChoosing()
        {
           if(CurrentCard !=null) CurrentCard.canChoose = false;
        }
        private void AddOrDiscardExtraCard(AddExtraCardToHandOrDiscard _addExtraCardToHandOrDiscard)
        {
            if(_addExtraCardToHandOrDiscard.whatHappens == WhatHappensExtraCard.Discard)
            {
                CurrentCard.discardFeedback.PlayFeedbacks();
                addCardToTheHand = null;
                CurrentCard = null;
            }
            else
            {
                _addExtraCardToHandOrDiscard.discardFeedback.PlayFeedbacks();
                CurrentCard.transform.SetParent(_addExtraCardToHandOrDiscard.parent);
                CurrentCard.transform.SetSiblingIndex(_addExtraCardToHandOrDiscard.siblingIndex);
                addCardToTheHand.Invoke(CurrentCard);
                CurrentCard= null;
                addCardToTheHand = null;
            }
            ChooseModeSignal.Trigger(false);
            textHelper.SetActive(false);
            discardButton.SetActive(false);

        }
    }

    public class ExtraCardCollectedSginal
    {
        public static event Action<Card,Action<Card>> OnCardCollected;
        public static void Trigger(Card card,Action<Card> addCardToTheHand)
        {
            OnCardCollected?.Invoke(card,addCardToTheHand);
        }
    }
    public class AddExtraCardToHandOrDiscard:ICommand
    {
        public WhatHappensExtraCard whatHappens;
        public Transform parent;
        public int siblingIndex;
        public MMF_Player discardFeedback;
    }
    public class ChooseModeSignal
    {
        public static event Action<bool> ChooseMode;
        public static void Trigger(bool choose)
        {
            ChooseMode?.Invoke(choose);
        }
    }
    public enum WhatHappensExtraCard
    {
        Discard,
        AddToHand
    }
}
