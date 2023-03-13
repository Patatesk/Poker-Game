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
            animator.SetBool(moveParameter, true);
            animator.SetBool(idleParameter,false);
        }
        public void IdleAnim()
        {
            animator.SetBool(idleParameter, true);
            animator.SetBool(moveParameter, false);
        }
        public void DeadAnim()
        {
            animator.SetBool(deadParameter, true);
            animator.SetBool(idleParameter, false);
            animator.SetBool(moveParameter, false);
        }


    }
}
