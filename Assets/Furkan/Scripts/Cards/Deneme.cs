using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace PK.PokerGame
{
    public class Deneme : MonoBehaviour
    {
        [SerializeField] private Transform paren;
        [SerializeField] HandChildHandler handChildHandler;

        [ContextMenu("DEneme")]
        public void SetParent()
        {
            transform.SetParent(paren);
            transform.SetSiblingIndex(handChildHandler.ReturnEmptySpace());
        }
    }
}
