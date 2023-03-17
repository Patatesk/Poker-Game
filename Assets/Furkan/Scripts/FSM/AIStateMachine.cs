using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class AIStateMachine : AIBrain
    {
        public HandManager handManager;
        public NavMeshAgent navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            handManager = GetComponent<HandManager>();
            navMeshAgent= GetComponent<NavMeshAgent>();
        }
    }
}
