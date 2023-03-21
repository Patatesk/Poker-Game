using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Player : MonoBehaviour
    {
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
            playerController.canMove= true;
            gameObject.tag = TagContainer.PlayerTag;
        }
        public void Lose()
        {
            animationController.DeadAnim();
            Debug.Log("LoseScreen");
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
        


    }
}
