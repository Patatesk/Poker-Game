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
        [SerializeField] private GameObject pos1Star;
        [SerializeField] private GameObject pos2Star;
        public void SetStars(int starCount)
        {
            switch (starCount)
            {
                case 1:
                    star1.SetActive(true);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    star1.transform.parent.position = pos1Star.transform.position;
                    break;
                case 2:
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(false);
                    star1.transform.parent.position = pos2Star.transform.position;

                    break;
                case 3:
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(true);
                    star1.transform.parent.position = new Vector3(0,pos1Star.transform.position.y, pos1Star.transform.position.z);
                    break;
            }
        }
    }
}
