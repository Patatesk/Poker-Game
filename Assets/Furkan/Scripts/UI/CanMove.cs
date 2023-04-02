using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CanMove : MonoBehaviour
    {
        [SerializeField] private GameObject obj;



        private void OnEnable()
        {
            GameStartSignal.gameStart += move;
        }
        private void OnDisable()
        {
            GameStartSignal.gameStart -= move;
        }
        private void move()
        {

            obj.SetActive(true);
        }
    }
}
