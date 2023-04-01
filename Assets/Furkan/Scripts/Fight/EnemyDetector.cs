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
        Transform thisObj;

        protected override void Awake()
        {
            base.Awake();
            thisObj = CheckifHasAParent.CheckParent(transform);
        }
        public override bool Decide()
        {
            return enemyDetected;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagContainer.AITag))
            {
                Transform otherObj = other.transform;
                if (other.transform.root == transform.root) return;
                gameObject.GetComponent<Collider>().enabled = false;
                AI ai = otherObj.transform.GetComponentInParent<AI>();
                ai.ForceToFight();
                AI self = this.transform.GetComponentInParent<AI>();
                self.ForceToFight();
                otherObj.gameObject.layer = 9;
                thisObj.gameObject.layer = 9;
                StartAIFightSignal.Trigger(ai, self);
                enemyDetected = true;
            }
            else if (other.transform.root.gameObject.layer == 8 && other.CompareTag(TagContainer.PlayerTag))
            {
                Transform otherObj = other.transform;

                if (Physics.Raycast(thisObj.position, thisObj.forward, 210))
                {
                    AI self = this.transform.GetComponentInParent<AI>();
                    Player player = otherObj.GetComponent<Player>();
                    if (self == null || player == null) return;
                    if (player.isfight) return;
                    gameObject.GetComponent<Collider>().enabled = false;
                    player.ForceToFight();
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
