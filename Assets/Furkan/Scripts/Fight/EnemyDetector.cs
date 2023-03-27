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
            Transform otherObj = CheckifHasAParent.CheckParent(other.transform);
            Transform thisObj = CheckifHasAParent.CheckParent(transform);
            if (isPlayer)
            {

                if (otherObj.CompareTag(TagContainer.AITag))
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    AI ai = otherObj.transform.root.GetComponent<AI>();
                    ai.ForceToFight();
                    otherObj.gameObject.layer = 9;
                    thisObj.gameObject.layer =  9;
                    StartFightSignal.Trigger(transform.root.GetComponent<Player>(), ai);
                    enemyDetected = true;
                }
            }
            else
            {
                if (otherObj.CompareTag(TagContainer.AITag))
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;

                    if (other.transform.root == transform.root) return;
                    AI ai = otherObj.transform.root.GetComponent<AI>();
                    ai.ForceToFight();
                    AI self = this.transform.root.GetComponent<AI>();
                    self.ForceToFight();
                    otherObj.gameObject.layer = 9;
                    thisObj.gameObject.layer = 9;
                    StartAIFightSignal.Trigger(ai, self);
                    enemyDetected = true;
                }
                else if (otherObj.CompareTag(TagContainer.PlayerTag))
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;

                    Player player = otherObj.GetComponent<Player>();
                    player.ForceToFight();
                    AI self = this.transform.root.GetComponent<AI>();
                    self.ForceToFight();
                    enemyDetected = true;
                    otherObj.gameObject.layer = 9;
                    thisObj.gameObject.layer = 9;
                    StartFightSignal.Trigger(player, self);
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
