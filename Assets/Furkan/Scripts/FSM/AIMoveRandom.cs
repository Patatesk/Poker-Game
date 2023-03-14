using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class AIMoveRandom : AIAction
    {
        [SerializeField] private Collider boundRange;
        [SerializeField] private NavMeshAgent agent;
        private AnimationController animator;
        public override void Initialization()
        {
            base.Initialization();
            animator = transform.root.GetComponent<AnimationController>();
        }
        public override void PerformAction()
        {
            if (!agent.hasPath) GetRandomPointsInBound();
            
        }

        private void GetRandomPointsInBound()
        {
            float x = Random.Range(boundRange.bounds.min.x, boundRange.bounds.max.x);
            float z = Random.Range(boundRange.bounds.min.z, boundRange.bounds.max.z);
            Vector3 move = new Vector3(x, transform.position.y, z);
            agent.SetDestination(move);
            animator.MoveAnim();
        }
    }
}
