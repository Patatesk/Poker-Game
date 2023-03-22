using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private float scaleUpAmount;
        private HandManager handManager;
        private AnimationController animationController;
        private AIStateMachine stateMachine;
        private EnemyDetector enemyDetector;
        private AIMove aIMove;
        private AIHandBuilder ýHandBuilder;

        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController= GetComponentInChildren<AnimationController>();
            stateMachine= GetComponentInChildren<AIStateMachine>();
            enemyDetector= GetComponentInChildren<EnemyDetector>();
            aIMove= GetComponentInChildren<AIMove>();
            ýHandBuilder= GetComponentInChildren<AIHandBuilder>();
        }

        public void FightEnded()
        {
            aIMove.ToggleCanMove(true);
            stateMachine.TransitionToState("Random");
            gameObject.tag = TagContainer.AITag;
        }
        public void Lose()
        {
            animationController.DeadAnim();
            Debug.Log("BirSüreSonraYokOlacak");
        }
        public void FightStarted()
        {
            handManager.BuildAIHand();
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
