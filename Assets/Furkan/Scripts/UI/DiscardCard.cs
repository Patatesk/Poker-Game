using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PK.Tools;

namespace PK.PokerGame
{
    public class DiscardCard : MonoBehaviour
    {
        private Mediator mediator;


        private void Awake()
        {
            mediator = GameObject.FindAnyObjectByType<Mediator>();
        }

        public void Discard()
        {
            AddExtraCardToHandOrDiscard discard = new AddExtraCardToHandOrDiscard();
            discard.whatHappens = WhatHappensExtraCard.Discard;
            mediator.Publish(discard);
        }
    }
}
