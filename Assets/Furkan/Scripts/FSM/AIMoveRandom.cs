using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class AIMoveRandom : MoreMountains.Tools.AIAction
    {
        public Collider boundRange;
        [SerializeField] private AIMove move;
        private  AIStateMachine stateMachine;
        protected override void Awake()
        {
            base.Awake();
           
            stateMachine = this.gameObject.GetComponentInParent<AIStateMachine>();

        }
        public override void Initialization()
        {
            base.Initialization();
        }
        public override void PerformAction()
        {
            if (!stateMachine.navMeshAgent.hasPath) GetRandomPointsInBound();
        }

        private void GetRandomPointsInBound()
        {
            if (!stateMachine.navMeshAgent.isActiveAndEnabled) return;
            float x = Random.Range(boundRange.bounds.min.x+10, boundRange.bounds.max.x-10);
            float z = Random.Range(boundRange.bounds.min.z+10, boundRange.bounds.max.z-10);
            Vector3 _move = new Vector3(x, transform.position.y, z);
            move.MoveToPos(_move);
        }
    }
}
