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
        [SerializeField] private RectTransform A�Hand;
        [SerializeField] private RectTransform playerHand;
        [SerializeField] private GameObject youWin, youLose,vs;
        private float a�StartPos;
        private float playerStartPos;

        private bool isFighting;
        
        private void Awake()
        {
            a�StartPos = A�Hand.anchoredPosition.y;
            playerStartPos = playerHand.anchoredPosition.y;
        }
        private void OnEnable()
        {
            StartFightSignal.FightStarter += StartSequence;
        }
        private void OnDisable()
        {
            StartFightSignal.FightStarter -= StartSequence;
        }
        private void StartSequence(Player player, AI fighterAI)
        {
            if (isFighting) return;
            StartCoroutine(StartFightSequence(player, fighterAI));
        }
        private IEnumerator StartFightSequence(Player player,AI fighterAI)
        {
            if (!isFighting)
            {
                isFighting = true;
                player.FightSarted();
                fighterAI.FightStarted();
                fightUI.SetActive(true);
                _camera.Priority = 15;
                Turn(player, fighterAI);

                yield return new WaitForSeconds(1f);
                A�Hand.DOAnchorPosY(-500, 1f);
                A�Hand.DOScale(1.2f, 1);
                playerHand.DOScale(1.2f, 1);
                playerHand.DOAnchorPosY(500, 1f);


                yield return new WaitForSeconds(1f);
                A�Hand.GetComponent<ChangeChild>().TurnFoward();

                yield return new WaitForSeconds(3f);
                vs.SetActive(false);
                FindWinner(player, fighterAI);

                A�Hand.DOAnchorPosY(a�StartPos, 1f);
                A�Hand.DOScale(1f, 1);
                playerHand.DOScale(1f, 1);
                playerHand.DOAnchorPosY(playerStartPos, 1f);


                yield return new WaitForSeconds(2);
                FightIsOver();
            }
        }

        private static void Turn(Player player, AI fighterAI)
        {
            Vector3 playerLookAtPos = fighterAI.transform.position - player.transform.position;
            Quaternion playerLookAtRot = Quaternion.LookRotation(playerLookAtPos);
            playerLookAtPos.x = 0;
            playerLookAtPos.y = 0;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerLookAtRot, 0.5f);
            Vector3 A�LookAtPos = player.transform.position - fighterAI.transform.position;
            Quaternion A�LookAtRot = Quaternion.LookRotation(A�LookAtPos);
            A�LookAtRot.x = 0;
            A�LookAtRot.y = 0;
            fighterAI.transform.rotation = Quaternion.Slerp(fighterAI.transform.rotation, A�LookAtRot, 0.5f);
            fighterAI.transform.DOLookAt(player.transform.position, .5f, AxisConstraint.X);
        }

        private void FindWinner(Player player, AI fighterAI)
        {
            if (player.HandRank() > fighterAI.HandRank())
            {
                youWin.SetActive(true);
                fighterAI.Lose();
                player.Win(2);
            }
            else if (player.HandRank() < fighterAI.HandRank())
            {
                youLose.SetActive(true);
                player.Lose();
                fighterAI.Win(2);
            }
            else
            {
                if (player.BiggestNumber() > fighterAI.BiggestNumber())
                {
                    fighterAI.Lose();
                    player.Win(2);
                    youWin.SetActive(true);
                }
                else
                {
                    youLose.SetActive(true);
                    player.Lose();
                    fighterAI.Win(2);
                }
            }
        }

        private void FightIsOver()
        {
            isFighting= false;
            _camera.Priority = 0;
            fightUI.SetActive(false);
            Destroy(A�Hand.GetChild(0).gameObject);
        }
    }


    public class StartFightSignal
    {
        public static event Action<Player, AI> FightStarter;
        public static void Trigger(Player player, AI fighterAI)
        {
            FightStarter?.Invoke(player, fighterAI);
        }
    }
}
