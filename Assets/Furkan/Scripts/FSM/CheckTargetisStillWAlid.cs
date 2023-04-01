using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace PK.PokerGame
{
    public class CheckTargetisStillWAlid : AIDecision
    {
        AIStateMachine stateMachine;

        protected override void Awake()
        {
            base.Awake();
            stateMachine= GetComponentInParent<AIStateMachine>();
        }
        public override bool Decide()
        {
            if(stateMachine.Target == null) return false;
           
            if (stateMachine.Target.gameObject.activeSelf) return true;
            else
            {
                stateMachine.Target = null;
                return false;
            }
        }

       
    }
}
