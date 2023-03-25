using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float scaleUpAmount = 0.15f;

        private HandManager handManager;
        private AnimationController animationController;
        private PlayerController playerController;
        private EnemyDetector enemyDetector;
        private PlayerStarHandler playerStarHandler;

        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController = GetComponentInChildren<AnimationController>();
            playerController= GetComponentInChildren<PlayerController>();
            enemyDetector= GetComponentInChildren<EnemyDetector>();
            playerStarHandler= GetComponentInChildren<PlayerStarHandler>();
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
            gameObject.tag = TagContainer.PlayerTag;
        }
        public void Lose()
        {
            animationController.DeadAnim();
            Debug.Log("LoseScreen");
        }
        public void Win(float time)
        {
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
