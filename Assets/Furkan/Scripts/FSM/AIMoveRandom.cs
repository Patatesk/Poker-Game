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
            float x = Random.Range(boundRange.bounds.min.x, boundRange.bounds.max.x);
            float z = Random.Range(boundRange.bounds.min.z, boundRange.bounds.max.z);
            Vector3 _move = new Vector3(x, transform.position.y, z);
            move.MoveToPos(_move);
        }
    }
}
