using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class PlayerCardCollector : MonoBehaviour
    {
        [SerializeField] private HandManager handManager;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Card"))
            {
                handManager.AddCardToHand(other.GetComponent<Card>());
            }
        }
    }
}
