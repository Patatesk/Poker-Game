using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class PlayerStarHandler : MonoBehaviour
    {
        [SerializeField] private GameObject star1;
        [SerializeField] private GameObject star2;
        [SerializeField] private GameObject star3;
        public void SetStars(int starCount)
        {
            switch (starCount)
            {
                case 1:
                    star1.SetActive(true);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    break;
                case 2:
                    star1.SetActive(false);
                    star2.SetActive(true);
                    star3.SetActive(false);

                    break;
                case 3:
                    star1.SetActive(false);
                    star2.SetActive(false);
                    star3.SetActive(true);
                    break;
            }
        }
    }
}
