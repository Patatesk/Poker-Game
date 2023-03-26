using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject winScreen;

        private void OnEnable()
        {
            LoseScreenSignal.OpenLoseScreen += LoseScreen;
            WinsScreenSginal.OpenWinScreen += WinScreen;
        }
        private void OnDisable()
        {
            LoseScreenSignal.OpenLoseScreen -= LoseScreen;
            WinsScreenSginal.OpenWinScreen -= WinScreen;
        }
        private void LoseScreen()
        {
            loseScreen.SetActive(true);
            winScreen.SetActive(false);
        }
        private void WinScreen()
        {
            loseScreen.SetActive(false);
            winScreen.SetActive(true);
        }
    }


    public class WinsScreenSginal
    {
        public static event Action OpenWinScreen;
        public static void Trigger()
        {
            OpenWinScreen?.Invoke();
        }
    }
    public class LoseScreenSignal
    {
        public static event Action OpenLoseScreen;
        public static void Trigger()
        {
            OpenLoseScreen?.Invoke();
        }
    }
}


