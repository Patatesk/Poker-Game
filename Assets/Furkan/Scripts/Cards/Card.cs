using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Card : MonoBehaviour
    {
        public Transform spawnPoint;
        public CardType cardType;
        public int cardValue;

        private void OnTriggerEnter(Collider other)
        {
            GetBackQueueSignal.Trigger(this);
            this.gameObject.SetActive(false);
        }
    }
}
