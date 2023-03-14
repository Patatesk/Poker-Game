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
        public void MoveAnim()
        {
            if (animator == null) return;
            animator.SetBool(moveParameter, true);
            animator.SetBool(idleParameter,false);
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
            animator.SetBool(deadParameter, true);
            animator.SetBool(idleParameter, false);
            animator.SetBool(moveParameter, false);
        }


    }
}
