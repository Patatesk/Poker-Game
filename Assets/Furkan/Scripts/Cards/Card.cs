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
        private bool useForUI;
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

        private void OnTriggerEnter(Collider other)
        {
            GetBackQueueSignal.Trigger(this);
            this.gameObject.SetActive(false);
        }
    }
}
