using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CheckForEnemy : AIDecision
    {
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private float checkRadius;
        [SerializeField] private HandManager cardManager;
        private AIStateMachine stateMachine;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = this.gameObject.GetComponentInParent<AIStateMachine>();
        }
        public override bool Decide()
        {
            if (stateMachine.handManager.totalCardCount < 5) return false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, enemyMask);

            if (colliders.Length != 0)
            {
                Transform target = colliders[0].transform;
                if (stateMachine.Target == null && target.gameObject.layer != gameObject.layer)
                    stateMachine.Target = colliders[0].transform;
                return true;
            }
            else return false;
        }
    }
}
