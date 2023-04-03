using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class PlayerCardCollector : MonoBehaviour
    {
        [SerializeField] private HandManager handManager;
        [SerializeField] private MMF_Player collectFeedBack;
     



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagContainer.CardTag))
            {
                Card card = other.GetComponent<Card>();
                card.ForceToCollect();
                handManager.AddCardToHand(card);
               
            }
        }
    }
}
