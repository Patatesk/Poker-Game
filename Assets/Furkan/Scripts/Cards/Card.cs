using DG.Tweening;
using MoreMountains.Feedbacks;
using PK.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PK.PokerGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        public Transform spawnPoint;
        public CardType cardType;
        public int cardValue;
        private Mediator mediator;
        private bool useForUI;
        public MMF_Player discardFeedback;
        [SerializeField] private MMF_Player forwardFaceAnim;
        public bool canChoose;
        public bool WinPirze = false;
        private float startPos;
        private bool selected;
        private Transform frontFace;
        private Transform backFace;
        private Collider _collider;
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
            frontFace = transform.GetChild(1).GetChild(1);
            backFace = transform.GetChild(1).GetChild(0);
            _collider = GetComponent<Collider>();
        }
        public void Discard()
        {
            DiscardCardSignal.Trigger(this);
            transform.SetParent(transform.root);
        }
        private void OnEnable()
        {
            ChooseModeSignal.ChooseMode += ChangeChooseMode;
            ResetCardSignal.ResetSignal += ResetCard;
            CanSelectableSignal.canSelectable += ChangeWinPrize;
            _collider.enabled = true;
        }
        private void OnDisable()
        {
            ChooseModeSignal.ChooseMode -= ChangeChooseMode;
            ResetCardSignal.ResetSignal -= ResetCard;
            CanSelectableSignal.canSelectable -= ChangeWinPrize;
        }

        public void FlipBackFace()
        {
            Transform child = transform.GetChild(1);
            child.GetChild(0).gameObject.SetActive(true);
            child.GetChild(1).gameObject.SetActive(false);
        }
        public void FlipForwardFace()
        {
            forwardFaceAnim.PlayFeedbacks();
        }
        private void ChangeChooseMode(bool mode)
        {
            canChoose = mode;
        }
        private void ChangeWinPrize(bool mode)
        {
            WinPirze = mode;
        }

        public void UnSelect()
        {
            frontFace.DOLocalMoveY(startPos, .5f);
            selected = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (canChoose)
            {
                AddExtraCardToHandOrDiscard addExtraCardToHandOrDiscard = new AddExtraCardToHandOrDiscard();
                addExtraCardToHandOrDiscard.parent = transform.parent;
                addExtraCardToHandOrDiscard.discardFeedback = discardFeedback;
                addExtraCardToHandOrDiscard.whatHappens = WhatHappensExtraCard.AddToHand;
                addExtraCardToHandOrDiscard.siblingIndex = transform.GetSiblingIndex();
                mediator.Publish(addExtraCardToHandOrDiscard);
            }
            if (WinPirze && !selected)
            {
                selected = true;
                ChangeCardDelivery delivery = new ChangeCardDelivery();
                delivery.card = this;
                delivery.siblingIndex = transform.GetSiblingIndex();
                delivery.isOwnedByPlayer = transform.parent.parent.GetComponent<DeckHandler>().isOwnedBuyPlayer;
                delivery.parent = transform.parent;
                delivery.totalCard = transform.parent.childCount - 5;
                mediator.Publish(delivery);
            }


        }

        public void Select(int value)
        {
            if (startPos == 0)
                startPos = frontFace.localPosition.y;
            backFace.gameObject.SetActive(false);
            frontFace.DOMoveY(frontFace.position.y + value, .5f);
        }

        private void ResetCard(Card card)
        {
            if (card == this) return;
            if (selected)
                frontFace.DOLocalMoveY(startPos, .5f);
            selected = false;


        }
        public void CollectedEvent()
        {
            GetBackQueueSignal.Trigger(this);
            this.gameObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagContainer.DefaultTag))
            {
                CollectedEvent();
            }

            if (other.CompareTag(TagContainer.PlayerTag))
            {
                _collider.enabled = false;
            }
        }
    }

    public class ResetCardSignal
    {
        public static event Action<Card> ResetSignal;
        public static void Trigger(Card selected)
        {
            ResetSignal?.Invoke(selected);
        }
    }
    public class CanSelectableSignal
    {
        public static event Action<bool> canSelectable;
        public static void Trigger(bool _canSelectable)
        {
            canSelectable?.Invoke(true);    
        }
    }
}
