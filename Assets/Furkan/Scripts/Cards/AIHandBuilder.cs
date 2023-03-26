using PK.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class AIHandBuilder : MonoBehaviour
    {
        private Transform parent;
        [SerializeField] private GameObject handPrefab;
        private HandChildHandler handChildHandler;
        private Mediator mediator;
        private bool coolDown;
        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
            parent = GameObject.FindWithTag("AIHand").transform;
        }
        private void OnEnable()
        {
            BuildAIHandSignal.HandBuilder += BuildHand;
        }

        private void OnDisable()
        {
            BuildAIHandSignal.HandBuilder -= BuildHand; 
        }
        private void BuildHand(List<Card> hand)
        {
            if (coolDown) return;
            coolDown = true;
            Invoke("CoolDown", 0.2f);
            HandChildHandler handObj = Instantiate(handPrefab,parent).GetComponent<HandChildHandler>();
            if(handChildHandler != null) Destroy(handChildHandler);
            handChildHandler= handObj;
            handObj.transform.GetComponent<RectTransform>().anchoredPosition= Vector3.zero;
            foreach (Card card in hand)
            {
                RequestCardFoAI(card,handObj);
            }     

        }
        private void CoolDown()
        {
            coolDown = false;
        }

        private void RequestCardFoAI(Card card,HandChildHandler hand)
        {
            RequestCard request= new RequestCard();
            request.cardValue = card.cardValue;
            request.type = card.cardType;
            request.hand = hand;
            request.isActive = false;
            mediator.Publish(request);
        }
        
    }

    public class BuildAIHandSignal
    {
        public static event Action<List<Card>> HandBuilder;
        public static void Trigger(List<Card> hand)
        {
            HandBuilder?.Invoke(hand);
        }
    }
}
