using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class HandNameShower : MonoBehaviour
    {
        [SerializeField] private GameObject[] Texts;

        private void OnEnable()
        {
            ShowHandNameSignal.ShowHandName += Open;
        }
        private void OnDisable()
        {
            ShowHandNameSignal.ShowHandName-= Open;
        }
        private void CloseAll()
        {
            foreach (GameObject go in Texts)
            {
                go.SetActive(false);
            }
        }

        private void Open(int hand)
        {
            if (hand == 1) return;
            CloseAll();
            Texts[hand-2].SetActive(true);
        }
    }

    public class ShowHandNameSignal
    {
        public static event Action<int> ShowHandName;
        public static void Trigger(int index)
        {
            ShowHandName?.Invoke(index);
        }
    }
}
