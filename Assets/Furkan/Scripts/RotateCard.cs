using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class RotateCard : MonoBehaviour
    {
        [SerializeField] private Vector3 rotateDirection;
        [SerializeField] private float speed;


        private void Update()
        {
            transform.Rotate(rotateDirection*speed*Time.deltaTime);
        }
    }
}
