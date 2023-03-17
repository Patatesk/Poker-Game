using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class AICheckForCards : AIDecision
    {
        [SerializeField] private LayerMask cardMask;
        [SerializeField] private float checkRadius;
        private AIStateMachine stateMachine;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = gameObject.GetComponentInParent<AIStateMachine>();
        }
        public override bool Decide()
        {
            if (stateMachine.handManager.totalCardCount > 5) return false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius,cardMask);

            if (colliders.Length != 0)
            {
                if(_brain.Target == null)
                _brain.Target = colliders[0].transform;
                return true;
            }
            else return false;
        }
    }
}
