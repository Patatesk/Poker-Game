using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class DeckHandler : MonoBehaviour
    {
        [SerializeField] public bool isOwnedBuyPlayer;
        private Transform child;
        
        private void Awake()
        {
            child = transform.GetChild(0);
        }
        public void FlibBackfaceAllTheCards()
        {
            for (int i = 0; i < child.childCount; i++)
            {
                Card _card = child.GetChild(i).GetComponent<Card>();
                if (_card != null)
                {
                    _card.FlipBackFace();
                    _card.gameObject.SetActive(true);
                }
            }
                
            
        }

        public void FlipAllCardsAnim()
        {
            StartCoroutine(FlipHandAnim());
        }


        private IEnumerator FlipHandAnim()
        {
            foreach (Transform _chil in child)
            {
                if (_chil.TryGetComponent<Card>(out Card _Card))
                {
                    _Card.FlipForwardFace();
                    yield return new WaitForSeconds(.2f);
                }
            }
        }
    }
}
