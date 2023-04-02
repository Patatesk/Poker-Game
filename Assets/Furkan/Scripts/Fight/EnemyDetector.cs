using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class EnemyDetector : AIDecision
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Transform rayPoint1;
        [SerializeField] private Transform rayPoint2;
        [SerializeField] private Transform rayPoint3;
        [SerializeField] private Transform rayPoint4;
        public bool enemyDetected;
        Transform thisObj;
        public bool isFighting;

        protected override void Awake()
        {
            base.Awake();
            thisObj = transform.parent;
        }
        public override bool Decide()
        {
            return enemyDetected;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isFighting) return;

            if (other.CompareTag(TagContainer.AITag) && other.gameObject.layer == 7)
            {
                Transform otherObj = other.transform;
                if (other.transform == transform.parent) return;
                gameObject.GetComponent<Collider>().enabled = false;
                AI ai = otherObj.transform.GetComponentInParent<AI>();
                ai.ForceToFight();
                AI self = this.transform.GetComponentInParent<AI>();
                self.ForceToFight();
                otherObj.gameObject.layer = 9;
                thisObj.gameObject.layer = 9;
                StartAIFightSignal.Trigger(ai, self);
                enemyDetected = true;
                isFighting = true;
            }
           


        }

        private void OnTriggerStay(Collider other)
        {
            if (isFighting) return;
            if (other.transform.root.gameObject.layer == 8 && other.CompareTag(TagContainer.PlayerTag))
            {
                Transform otherObj = other.transform;

                if (Physics.Raycast(thisObj.position, thisObj.forward, 2, mask) 
                    || Physics.Raycast(rayPoint1.position, rayPoint1.forward, 2, mask) ||
                    Physics.Raycast(rayPoint2.position, rayPoint2.forward, 2, mask)
                    || Physics.Raycast(rayPoint3.position, rayPoint3.forward, 2, mask)
                    || Physics.Raycast(rayPoint4.position, rayPoint4.forward, 2, mask))
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
                    isFighting = true;

                }
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.parent.position,transform.parent.forward*2);
            Gizmos.DrawRay(rayPoint1.position,rayPoint1.forward*2);
            Gizmos.DrawRay(rayPoint2.position,rayPoint2.forward*2);
        }
        public override void OnExitState()
        {
            base.OnExitState();
            enemyDetected = false;
        }
    }
}
