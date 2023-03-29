using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class Player : MonoBehaviour
    {
        public bool isfight;

        [SerializeField] private float scaleUpAmount = 0.15f;
        [SerializeField] private int basicIncomePerWin= 100;
        [SerializeField] private MMF_Player winFeedBack;
        [SerializeField] private MMF_Player loseFeedBack;
        
        private HandManager handManager;
        private AnimationController animationController;
        private PlayerController playerController;
        private PlayerFightStarter fightStarter;

        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController = GetComponentInChildren<AnimationController>();
            playerController= GetComponentInChildren<PlayerController>();
            fightStarter = GetComponentInChildren<PlayerFightStarter>();
        }

        private void Update()
        {
            if (fightStarter.fightStarted)
            {
                playerController.canMove= false;
                animationController.IdleAnim();
            }
        }

        public void Fightchange(bool value)
        {
            isfight=value;
        }
        
        public void FightEnded()
        {
            fightStarter.fightStarted= false;
            playerController.canMove= true;
            gameObject.layer = 8;
            fightStarter.gameObject.layer = 12;
            fightStarter.GetComponent<Collider>().enabled = true;
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
            fightStarter.GetComponent<Collider>().enabled = true;
        }
        public void Lose()
        {
            loseFeedBack.PlayFeedbacks();
            animationController.DeadAnim();
            fightStarter.GetComponent<Collider>().enabled = false;
            Invoke("LoseGame", 3);

        }

        private void LoseGame()
        {
            LoseScreenSignal.Trigger();

        }
        public void Win(float time)
        {
            animationController.Victory();
            fightStarter.ResetFight();
            winFeedBack.PlayFeedbacks();
            AddMoneySignal.Trigger(basicIncomePerWin);

        }
        
        public void FightSarted()
        {
            Fightchange(true);
            fightStarter.gameObject.layer = 9;
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
            fightStarter.fightStarted = true;
            animationController.IdleAnim();
            playerController.canMove = false;
        }

        public void AddCard(Card card)
        {
            handManager.AddCardToHandRankHand(card);
            ScaleUp(TotalCardCount());
        }

    }
}
