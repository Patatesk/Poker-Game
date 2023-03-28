using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
using System;

namespace PK.PokerGame
{
    public class PlayerCurrencyHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mainMoney;
        [SerializeField] private TextMeshProUGUI tempMoney;
        [SerializeField] private MMF_Player addMoneyFeedback;
        [SerializeField] private MMF_TMPCountTo countTo;

        private int money;
        private void Awake()
        {
            countTo = addMoneyFeedback.GetFeedbackOfType<MMF_TMPCountTo>();
        }

        private void OnEnable()
        {
            AddMoneySignal.addMoney += AddMoney;
        }
        private void OnDisable()
        {
            AddMoneySignal.addMoney -= AddMoney;
        }

        private void AddMoney(int value)
        {
            countTo.CountFrom = money;
            countTo.CountTo = value;
            tempMoney.text= value.ToString();
            addMoneyFeedback.PlayFeedbacks();
            while (addMoneyFeedback.HasFeedbackStillPlaying())
            {

            }
            money = int.Parse(mainMoney.text);
            
        }


    }

    public class AddMoneySignal
    {
        public static event Action<int> addMoney;
        public static void Trigger(int value)
        {
            addMoney?.Invoke(value);
        }
    }
}
