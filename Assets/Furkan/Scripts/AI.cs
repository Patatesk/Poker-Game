using DG.Tweening;
using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PK.PokerGame
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private float scaleUpAmount;
        [SerializeField] private MMF_Player dieFeedBack;
        private Collider bound;
        private HandManager handManager;
        private AnimationController animationController;
        private AIStateMachine stateMachine;
        private EnemyDetector enemyDetector;
        private AIMove aIMove;
        private AIHandBuilder ýHandBuilder;
        private Collider[] colliders;
        private AIMoveRandom random;
        private CheckFightIsOver checkFight;
        private NavMeshAgent agent;
        public bool inFight;
        private void Awake()
        {
            handManager = GetComponentInChildren<HandManager>();
            animationController= GetComponentInChildren<AnimationController>();
            stateMachine= GetComponentInChildren<AIStateMachine>();
            enemyDetector= GetComponentInChildren<EnemyDetector>();
            aIMove= GetComponentInChildren<AIMove>();
            ýHandBuilder= GetComponentInChildren<AIHandBuilder>();
            random = GetComponentInChildren<AIMoveRandom>();
            colliders = GetComponentsInChildren<Collider>();
            checkFight = GetComponentInChildren<CheckFightIsOver>();
            bound = GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider>();
            agent = GetComponentInChildren<NavMeshAgent>();
            stateMachine.enabled= false;
        }
        private IEnumerator Start()
        {
            random.boundRange = bound;
            yield return new WaitForSeconds(0.2f);
            AddAISignal.Trigger(transform);

        }

        private void OnEnable()
        {
            GameStartSignal.gameStart += GameStart;
        }
        private void OnDisable()
        {
            GameStartSignal.gameStart -= GameStart;
        }

        private void GameStart()
        {
            stateMachine.enabled= true;
        }
        
        public void TouchedObstackle()
        {
            gameObject.layer = 10;
            aIMove.Obstackle();
            Invoke("ChangeLayer", 10);
        }

        private void ChangeLayer()
        {
            if (inFight) return; 
            gameObject.layer =7;
        }

        public void FightEnded()
        {
            inFight = false;
            checkFight.fightIsOver = true;
            aIMove.ToggleCanMove(true);
            stateMachine.TransitionToState("Random");
            ChangeLayer();
            enemyDetector.GetComponent<Collider>().enabled = true;
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
            enemyDetector.isFighting = false;
        }
        public void Lose()
        {
            animationController.DeadAnim();
            animationController.CloseRootMotion();
            dieFeedBack.PlayFeedbacks();
            enemyDetector.GetComponent<Collider>().enabled = false;
            DeadAISignal.Trigger(transform);
        }
        public void FightStarted()
        {
            inFight = true;
            handManager.BuildAIHand();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
        public void AIFightStarted()
        {
            checkFight.fightIsOver = false;
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
            ForceToFight();

        }
        public void Win(float time)
        {
            agent.isStopped = false;
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
            agent.isStopped = true;
            aIMove.ToggleCanMove(false);
            checkFight.fightIsOver = false;
            enemyDetector.enemyDetected = true;
            animationController.IdleAnim();
           
        }
       
    }

    public static class GameStartSignal
    {
        public static event Action gameStart;
        public static void Trigger()
        {
            gameStart?.Invoke();
        }
    }
}
