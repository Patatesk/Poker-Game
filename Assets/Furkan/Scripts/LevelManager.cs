using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PK.PokerGame
{
    public class LevelManager : MonoBehaviour
    {
        private int totalAICaount;
        private int aliveAICount;


        private void OnEnable()
        {
            AddAISignal.AddAI += AddAI;
            DeadAISignal.DeadAI+= DeadAI;
        }
        private void OnDisable()
        {

            AddAISignal.AddAI -= AddAI;
            DeadAISignal.DeadAI -= DeadAI;
        }

        private void AddAI()
        {
            totalAICaount++;
            aliveAICount++;

        }
        public void NextScene()
        {
            if(SceneManager.sceneCountInBuildSettings -1 >= SceneManager.GetActiveScene().buildIndex + 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void DeadAI()
        {
            aliveAICount--;
            if(aliveAICount == 0)
            {
                WinsScreenSginal.Trigger();
            }
        }
    }

    public class AddAISignal
    {
        public static event Action AddAI;
        public static void Trigger()
        {
            AddAI?.Invoke();
        }
    }

    public class DeadAISignal
    {
        public static event Action DeadAI;
        public static void Trigger()
        {
            DeadAI?.Invoke();
        }
    }
}
