using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float scaleUpAmount = 0.15f;
        [SerializeField] private MMF_Player winFeedBack;
        [SerializeField] private MMF_Player loseFeedBack;
        
        private HandManager handManager;
        private AnimationController animationController;
        private PlayerController playerController;
        private EnemyDetector enemyDetector;

        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController = GetComponentInChildren<AnimationController>();
            playerController= GetComponentInChildren<PlayerController>();
            enemyDetector= GetComponentInChildren<EnemyDetector>();
        }

        private void Update()
        {
            if (enemyDetector.Decide())
            {
                playerController.canMove= false;
                animationController.IdleAnim();
            }
        }
        
        public void FightEnded()
        {
            enemyDetector.enemyDetected= false;
            playerController.canMove= true;
            gameObject.layer = 8;
            enemyDetector.GetComponent<Collider>().enabled = true;

        }
        public void TouchedObstackle()
        {
            gameObject.layer = 10;
            playerController.Obstackle();
            Invoke("ChangeLayer", 10);
            animationController.CloseRootMotion();
        }

        private void ChangeLayer()
        {
            gameObject.layer =8;
            playerController.applyGravity = true;
            animationController.OpenRootMotion();
            enemyDetector.GetComponent<BoxCollider>().enabled = true;
        }
        public void Lose()
        {
            loseFeedBack.PlayFeedbacks();
            animationController.DeadAnim();
            LoseScreenSignal.Trigger();
            enemyDetector.GetComponent<BoxCollider>().enabled = false;
        }
        public void Win(float time)
        {
            winFeedBack.PlayFeedbacks();
            Invoke("FightEnded", time);

        }
        public void FightSarted()
        {
    

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
            enemyDetector.enemyDetected= true;
            animationController.IdleAnim();
            playerController.canMove = false;
        }

        public void AddCard(Card card)
        {
            handManager.AddCardToHandRankHand(card);
        }

    }
}
