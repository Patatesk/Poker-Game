using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace PK.PokerGame
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GameObject fightUI;
        [SerializeField] private RectTransform AýHand;
        [SerializeField] private GameObject youWin, youLose,vs;

        private bool isFighting;
        private void OnEnable()
        {
            StartFightSignal.FightStarter += StartSequence;
        }
        private void OnDisable()
        {
            StartFightSignal.FightStarter -= StartSequence;
        }
        private void StartSequence(GameObject player, GameObject fighterAI)
        {
            if (isFighting) return;
            StartCoroutine(StartFightSequence(player, fighterAI));
        }
        private IEnumerator StartFightSequence(GameObject player,GameObject fighterAI)
        {
            if (!isFighting)
            {
                HandManager playerHand = player.GetComponent<HandManager>();
                HandManager _AIHand = fighterAI.GetComponent<HandManager>();

                isFighting = true;
                player.GetComponent<AnimationController>().IdleAnim();
                player.GetComponent<PlayerController>().canMove = false;
                _AIHand.BuildAIHand();
                fighterAI.GetComponentInChildren<EnemyDetector>().enemyDetected = true;
                fightUI.SetActive(true);
                _camera.Priority = 15;
                yield return new WaitForSeconds(1f);
                AýHand.DOAnchorPosY(-256, 1f);
                yield return new WaitForSeconds(3f);
                vs.SetActive(false);
                if (playerHand.handRank > _AIHand.handRank)
                {
                    youWin.SetActive(true);
                }
                else if(playerHand.handRank < _AIHand.handRank)
                {
                    youLose.SetActive(true);
                }
                else
                {
                    if (playerHand.ranksBiggestNumber > _AIHand.ranksBiggestNumber) youWin.SetActive(true);
                    else youLose.SetActive(true);
                }
                yield return new WaitForSeconds(2);
                FightIsOver();
            }
        }


        private void FightIsOver()
        {
            isFighting= false;
            _camera.Priority = 0;
            fightUI.SetActive(false);
        }
    }


    public class StartFightSignal
    {
        public static event Action<GameObject, GameObject> FightStarter;
        public static void Trigger(GameObject player,GameObject fighterAI)
        {
            FightStarter?.Invoke(player, fighterAI);
        }
    }
}
