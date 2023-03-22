using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class ChangeChild : MonoBehaviour
    {
        private DeckHandler deck;
        private void OnTransformChildrenChanged()
        {
            Invoke("FlipCards", .5f);
        }

        private void FlipCards()
        {
            if (transform.childCount >= 1)
            {
                deck = transform.GetChild(0).GetComponent<DeckHandler>();
                deck.FlibBackfaceAllTheCards();
            }
        }

        public void TurnFoward()
        {
            deck.FlipAllCardsAnim();
        }

    }
}
