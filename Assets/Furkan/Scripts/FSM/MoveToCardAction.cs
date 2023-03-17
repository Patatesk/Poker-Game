using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class MoveToCardAction : AIAction
    {
        private AIMove move;
        private AIStateMachine stateMachine;

        protected override void Awake()
        {
            stateMachine = this.gameObject.GetComponentInParent<AIStateMachine>();
            _brain = stateMachine;
            move = gameObject.GetComponentInParent<AIMove>();
        }
        public override void Initialization()
        {
            base.Initialization();
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            stateMachine.navMeshAgent.ResetPath();
        }
        public override void PerformAction()
        {
            if (_brain.Target != null && !stateMachine.navMeshAgent.hasPath)
            {
                move.ChangeTargetandMove(_brain.Target);
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Card"))
            {
                _brain.Target= null;
                stateMachine.navMeshAgent.ResetPath();
                stateMachine.handManager.AddCardToHand(other.GetComponent<Card>());
            }
        }


    }
}
