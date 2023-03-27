using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PK.PokerGame
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string moveParameter;
        [SerializeField] private string idleParameter;
        [SerializeField] private string deadParameter;
        [SerializeField] private string locomotionParameter;
        [SerializeField] private string Fall = "Fall";
        [SerializeField] private float animOfsset;

        private void Start()
        {
            animator.applyRootMotion = true;
        }

        public Vector3 GetRootPos()
        {
            Vector3 rootPos = animator.rootPosition;
            rootPos.y = animOfsset;
            return rootPos;
        }
        public void MoveAnim(bool shouldmove = true, float value = 0)
        {
            if (animator == null) return;
            animator.SetBool(moveParameter, shouldmove);
            animator.SetBool(idleParameter,!shouldmove);
            animator.SetFloat(locomotionParameter,value);
        }
        public void IdleAnim()
        {
            if (animator == null) return;
            animator.SetBool(idleParameter, true);
            animator.SetBool(moveParameter, false);
        }
        public void DeadAnim()
        {
            if (animator == null) return;
            animator.SetTrigger(deadParameter);
            animator.SetBool(idleParameter, false);
            animator.SetBool(moveParameter, false);
        }
        public void FallAnim()
        {
            animator.SetBool(idleParameter, false);
            animator.SetBool(moveParameter, false);
            animator.SetTrigger(Fall);
        }
        public void CloseRootMotion()
        {
            animator.applyRootMotion = false;
        } 
        public void OpenRootMotion()
        {
            animator.applyRootMotion = true;
        }


    }
}
