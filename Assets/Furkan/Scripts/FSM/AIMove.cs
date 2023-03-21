using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class AIMove : MonoBehaviour
    {
        private Transform target;
        private Vector3 targetPos;
        [SerializeField] private float shouldMoveTreshhold = 0.5f;
        private NavMeshAgent agent;
        private AnimationController animController;
        private Vector2 Velocity;
        private Vector2 SmoothDeltaPosition;
        private bool canMove = true;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animController= GetComponent<AnimationController>();
            agent.updatePosition = false;
            agent.updateRotation = true;
        }

        private void OnAnimatorMove()
        {
            Vector3 rootPosition = animController.GetRootPos();
            rootPosition.y = agent.nextPosition.y;
            transform.position = rootPosition;
            agent.nextPosition = rootPosition;
        }

        public void ToggleCanMove(bool move)
        {
            canMove= move;
            agent.enabled= move;
        }

        private void Update()
        {
            if (!canMove) return;
            SynchronizeAnimatorAndAgent();
        }

        private void SynchronizeAnimatorAndAgent()
        {
            Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
            worldDeltaPosition.y = 0;

            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
            SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

            Velocity = SmoothDeltaPosition / Time.deltaTime;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Velocity = Vector2.Lerp(
                    Vector2.zero,
                    Velocity,
                    agent.remainingDistance / agent.stoppingDistance
                );
            }

            bool shouldMove = Velocity.magnitude > shouldMoveTreshhold
                && agent.remainingDistance > agent.stoppingDistance;

            animController.MoveAnim(shouldMove, Velocity.magnitude);

            float deltaMagnitude = worldDeltaPosition.magnitude;
            if (deltaMagnitude > agent.radius / 2f)
            {
                transform.position = Vector3.Lerp(
                    animController.GetRootPos(),
                    agent.nextPosition,
                    smooth
                );
            }
        }
        public void MoveToPos(Vector3 pos)
        {
            targetPos = pos;
            agent.SetDestination(targetPos);
        }
        public void ChangeTargetandMove(Transform _target)
        {
            
            target = _target;
            targetPos = target.position;
            agent.SetDestination(target.position);
        }
    }
}
