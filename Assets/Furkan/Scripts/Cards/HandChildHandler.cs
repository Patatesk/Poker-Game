using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class HandChildHandler : MonoBehaviour
    {
        [SerializeField] private List<Transform> handEmptyPos = new List<Transform>();
        int request = 5;

        public int ReturnEmptySpace()
        {

            if (request == 5)
            {
                handEmptyPos[2].SetAsLastSibling();
                request--;
                return 2;
            }
            else if (request == 4)
            {
                handEmptyPos[3].SetAsLastSibling();
                request--;
                return 3;
            }
            else if (request == 3)
            {
                handEmptyPos[1].SetAsLastSibling();
                request--;
                return 1;
            }
            else if (request == 2)
            {
                handEmptyPos[4].SetAsLastSibling();
                request--;
                return 4;
            }
            else{
                handEmptyPos[0].SetAsLastSibling();
                request--;
                return 0;
            }

        }

    }
}
