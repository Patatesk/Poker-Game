using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using MoreMountains.Feedbacks;

namespace PK.PokerGame
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GameObject fightUI;
        [SerializeField] private RectTransform AýHand;
        [SerializeField] private RectTransform playerHand;
        [SerializeField] private GameObject youWin, youLose,vs,pickAcard,Select2Cards;
        [SerializeField] private GameObject extraCardParts;
        [SerializeField] private GameObject discardButton;
        [SerializeField] private MMF_Player Select2CardsFeedback;
        [SerializeField] private MMF_Player Select1CardFeedback;

        private float discardTime = 1f;
        private float aýStartPos;
        private float playerStartPos;
        private bool isFighting;
        private float _discardTime;

        public bool changeCard;

        private void Awake()
        {
            aýStartPos = AýHand.anchoredPosition.y;
            playerStartPos = playerHand.anchoredPosition.y;
            _discardTime= discardTime;
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
        public void ChangeDiscardTime(float time)
        {
            _discardTime = time;
        }
        private IEnumerator StartFightSequence(Player player,AI fighterAI)
        {
            if (!isFighting)
            {
               
                extraCardParts.SetActive(false);
                isFighting = true;
                player.FightSarted();
                fighterAI.FightStarted();
                fightUI.SetActive(true);
                _camera.Priority = 15;
                Turn(player, fighterAI);

                yield return new WaitForSeconds(1f);
                AýHand.DOAnchorPosY(-500, 1f);
                AýHand.DOScale(1.2f, 1);
                playerHand.DOScale(1.2f, 1);
                playerHand.DOAnchorPosY(500, 1f);


                yield return new WaitForSeconds(1f);
                AýHand.GetComponent<ChangeChild>().TurnFoward();

                yield return new WaitForSeconds(3f);
                vs.SetActive(false);
                string winner = FindWinner(player, fighterAI);
                if(winner == "Player")
                {
                    discardButton.SetActive(true);
                    CanSelectableSignal.Trigger(true);
                    yield return new WaitUntil( () => changeCard == true);
                    pickAcard.SetActive(false);
                    Select2Cards.SetActive(false);
                    player.Win(2);
                    fighterAI.Lose();
                }
                else
                {
                    player.Lose();
                    fighterAI.Win(2);
                }

                yield return new WaitForSeconds(_discardTime);
                AýHand.DOAnchorPosY(aýStartPos, 1f);
                AýHand.DOScale(1f, 1);
                playerHand.DOScale(1f, 1);
                playerHand.DOAnchorPosY(playerStartPos, 1f);


                yield return new WaitForSeconds(2);
                FightIsOver();
            }
        }
        
        public void CardsChanged()
        {
            changeCard = true;
        }
        
        private static void Turn(Player player, AI fighterAI)
        {
            Vector3 playerLookAtPos = fighterAI.transform.position - player.transform.position;
            Quaternion playerLookAtRot = Quaternion.LookRotation(playerLookAtPos);
            playerLookAtPos.x = 0;
            playerLookAtPos.y = 0;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerLookAtRot, 0.5f);
            Vector3 AýLookAtPos = player.transform.position - fighterAI.transform.position;
            Quaternion AýLookAtRot = Quaternion.LookRotation(AýLookAtPos);
            AýLookAtRot.x = 0;
            AýLookAtRot.y = 0;
            fighterAI.transform.rotation = Quaternion.Slerp(fighterAI.transform.rotation, AýLookAtRot, 0.5f);
        }

        private string FindWinner(Player player, AI fighterAI)
        {
            if (player.HandRank() > fighterAI.HandRank())
            {
                if(player.TotalCardCount() < 5)
                {
                    Select1CardFeedback.PlayFeedbacks();
                }
                else
                {
                    Select2CardsFeedback.PlayFeedbacks();
                }
               
                return "Player";
            }
            else if (player.HandRank() < fighterAI.HandRank())
            {
                youLose.SetActive(true);
                
                return "AI";
            }
            else
            {
                if (player.BiggestNumber() > fighterAI.BiggestNumber())
                {


                    if (player.TotalCardCount() < 5)
                    {
                        Select1CardFeedback.PlayFeedbacks();
                    }
                    else
                    {
                        Select2CardsFeedback.PlayFeedbacks();
                    }
                    return "Player";
                }
                else
                {
                    youLose.SetActive(true);
                    
                    return "AI";
                }
            }
        }

        private void FightIsOver()
        {
            isFighting= false;
            _camera.Priority = 0;
            fightUI.SetActive(false);
            changeCard = false;
            Destroy(AýHand.GetChild(0).gameObject);
            extraCardParts.SetActive(true);
            discardButton.SetActive(false);

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
