using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Card : MonoBehaviour
    {
        public Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            GetBackQueueSignal.Trigger(this);
            this.gameObject.SetActive(false);
        }
    }
}
