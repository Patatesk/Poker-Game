using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class ArrowIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;
        private bool canShowLastAI;
        private Transform target;

        private void OnEnable()
        {
            ShowIndicatorSginal.show += Set;
        }
        private void OnDisable()
        {
            ShowIndicatorSginal.show -= Set;

        }

        private void Set(bool _show, Transform _target)
        {
            this.canShowLastAI = _show;
            this.target = _target;
            indicator.SetActive(true);
        }
        private void Update()
        {
            if (target == null || !canShowLastAI) return;
            indicator.transform.LookAt(target.position,indicator.transform.up);
        }

    }

    public class ShowIndicatorSginal
    {
        public static event Action<bool, Transform> show;
        public static void Trigger(bool _show,Transform target)
        {
            show?.Invoke(_show,target);
        }
    }
}
