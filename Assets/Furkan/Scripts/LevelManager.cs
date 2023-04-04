using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PK.PokerGame
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI AýCounter;
        private int totalAICaount;
        private int aliveAICount;
        private List<Transform> ailist = new List<Transform>();

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

        private void AddAI(Transform ai)
        {
            totalAICaount++;
            aliveAICount++;
            SetAýCounterText();
            ailist.Add(ai);
        }

        private void SetAýCounterText()
        {
            AýCounter.text =  aliveAICount + "/" +totalAICaount.ToString();
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

        private void DeadAI(Transform ai)
        {
            aliveAICount--;
            SetAýCounterText();
            ailist.Remove(ai);
            if (aliveAICount == 1) ShowIndicatorSginal.Trigger(true, ailist[0]);
            if (aliveAICount == 0)
            {
                WinsScreenSginal.Trigger();
            }
        }
    }

    public class AddAISignal
    {
        public static event Action<Transform> AddAI;
        public static void Trigger(Transform ai)
        {
            AddAI?.Invoke(ai);
        }
    }

    public class DeadAISignal
    {
        public static event Action<Transform> DeadAI;
        public static void Trigger(Transform ai)
        {
            DeadAI?.Invoke(ai);
        }
    }
}
