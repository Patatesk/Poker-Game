using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

namespace PK.PokerGame
{
    public class FightManager : MonoBehaviour
    {
       
        [SerializeField] private GameObject fightUI;

        private bool isFighting;
        private void OnEnable()
        {
            StartFightSignal.FightStarter += StartFightSequence;
        }
        private void OnDisable()
        {
            StartFightSignal.FightStarter -= StartFightSequence;
        }
        private void StartFightSequence(GameObject player,GameObject fighterAI)
        {
            if (isFighting) return; isFighting = true;
            player.GetComponent<AnimationController>().IdleAnim();
            player.GetComponent<PlayerController>().canMove = false;
            fighterAI.GetComponent<HandManager>().BuildAIHand();
            fighterAI.GetComponentInChildren<EnemyDetector>().enemyDetected = true;
            fightUI.SetActive(true);
        }


        private void FightIsOver()
        {
            isFighting= false;
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
