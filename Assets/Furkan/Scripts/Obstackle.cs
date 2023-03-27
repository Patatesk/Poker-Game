using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Obstackle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagContainer.AITag))
            {
                AI ai = other.GetComponent<AI>();
                if (ai != null)
                ai.TouchedObstackle();
            }
            if (other.CompareTag(TagContainer.PlayerTag))
            {
                Player player= other.GetComponent<Player>();
                if (player != null)
                player.TouchedObstackle();    
            }
        }
    }
}
