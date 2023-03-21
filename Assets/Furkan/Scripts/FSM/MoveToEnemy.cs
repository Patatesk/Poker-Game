using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class MoveToEnemy : MoreMountains.Tools.AIAction
    {
        [SerializeField] private AIMove move;
        
        public override void Initialization()
        {
            base.Initialization();
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
        }
        public override void PerformAction()
        {
            if (_brain.Target != null)
            {
                move.ChangeTargetandMove( _brain.Target);
            }

        }
    }
}
