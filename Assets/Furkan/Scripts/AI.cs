using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private float scaleUpAmount;
        [SerializeField] private MMF_Player dieFeedBack;
        private HandManager handManager;
        private AnimationController animationController;
        private AIStateMachine stateMachine;
        private EnemyDetector enemyDetector;
        private AIMove aIMove;
        private AIHandBuilder ýHandBuilder;
        private Collider[] colliders;

        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController= GetComponentInChildren<AnimationController>();
            stateMachine= GetComponentInChildren<AIStateMachine>();
            enemyDetector= GetComponentInChildren<EnemyDetector>();
            aIMove= GetComponentInChildren<AIMove>();
            ýHandBuilder= GetComponentInChildren<AIHandBuilder>();
            colliders = GetComponentsInChildren<Collider>();
        }

        public void FightEnded()
        {
            aIMove.ToggleCanMove(true);
            stateMachine.TransitionToState("Random");
            gameObject.tag = TagContainer.AITag;
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }
        public void Lose()
        {
            animationController.DeadAnim();
            Debug.Log("BirSüreSonraYokOlacak");
            dieFeedBack.PlayFeedbacks();
        }
        public void FightStarted()
        {
            handManager.BuildAIHand();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
        public void Win(float time)
        {
            Invoke("FightEnded", time);
        }
        public int HandRank()
        {
            return handManager.handRank;
        }
        public int BiggestNumber()
        {
            return handManager.ranksBiggestNumber;
        }

        public int TotalCardCount()
        {
            return handManager.totalCardCount;
        }
        public void ScaleUp(int totalCardCoun)
        {
            Vector3 newScale = new Vector3(1 + (scaleUpAmount * totalCardCoun), 1 + (scaleUpAmount * totalCardCoun), 1 + (scaleUpAmount * totalCardCoun));
            transform.GetChild(0).DOScale(newScale, .5f);
        }

        public void ForceToFight()
        {
            stateMachine.TransitionToState("Idle");
            enemyDetector.enemyDetected = true;
            animationController.IdleAnim();
            aIMove.ToggleCanMove(false);
        }
    }
}
