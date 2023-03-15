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
        private AnimationController animator;
        public override void Initialization()
        {
            base.Initialization();
            animator = transform.root.GetComponent<AnimationController>();
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
                animator.MoveAnim();
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Card"))
            {
                //other.gameObject.SetActive(false);
                _brain.Target= null;
                agent.ResetPath();
            }
        }


    }
}
