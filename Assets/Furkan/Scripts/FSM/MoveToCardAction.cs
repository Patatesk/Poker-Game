using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class MoveToCardAction : AIAction
    {
        [SerializeField] private NavMeshAgent agent;
        public override void Initialization()
        {
            base.Initialization();
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            agent.ResetPath();
        }
        public override void PerformAction()
        {
            if (_brain.Target != null && !agent.hasPath)
            {
                agent.SetDestination(_brain.Target.position);
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Card"))
            {
                other.gameObject.SetActive(false);
                _brain.Target= null;
                agent.ResetPath();
            }
        }


    }
}
