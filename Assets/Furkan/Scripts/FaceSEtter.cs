using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class FaceSEtter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer first;
        [SerializeField] private SpriteRenderer second;

        private void Awake()
        {
            second.sprite = first.sprite;
        }
    }
}
