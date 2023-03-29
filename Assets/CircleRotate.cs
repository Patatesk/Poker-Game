using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CircleRotate : MonoBehaviour
    {
        [SerializeField] private float hýz;
        void Update()
        {
            transform.Rotate(0, 0, hýz*Time.deltaTime);
        }
    }
}
