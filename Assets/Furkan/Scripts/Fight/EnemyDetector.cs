using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class EnemyDetector : AIDecision
    {
        [SerializeField] private bool isPlayer;
        public bool enemyDetected;
        public override bool Decide()
        {
            return enemyDetected;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isPlayer)
            {
                if (other.CompareTag(TagContainer.AITag))
                {
                    other.transform.root.tag = TagContainer.DontDisturbTag;
                    tag =  TagContainer.DontDisturbTag;
                    StartFightSignal.Trigger(transform.root.gameObject, other.transform.root.gameObject);
                    enemyDetected = true;
                }
            }
            else
            {
                if (other.CompareTag(TagContainer.AITag))
                {
                    other.transform.root.tag = TagContainer.DontDisturbTag;
                    transform.root.tag = TagContainer.DontDisturbTag;
                    
                    enemyDetected = true;
                }
                else if (other.CompareTag(TagContainer.PlayerTag))
                {
                    enemyDetected = true;
                    other.tag = TagContainer.DontDisturbTag;
                    transform.root.tag = TagContainer.DontDisturbTag;
                    StartFightSignal.Trigger(other.gameObject, transform.root.gameObject);
                }
            }

        }
        public override void OnExitState()
        {
            base.OnExitState();
            enemyDetected = false;
        }
    }
}
