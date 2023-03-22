using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace PK.PokerGame
{
    public class IDle : MoreMountains.Tools.AIAction
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
            //stateMachine.navMeshAgent.ResetPath();
            //move.ToggleCanMove(false);
            //GetComponent<AnimationController>().IdleAnim();

        }
        public override void PerformAction()
        {
           

        }
       
    }
}
