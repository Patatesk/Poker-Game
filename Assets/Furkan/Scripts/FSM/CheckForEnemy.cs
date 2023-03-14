using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CheckForEnemy : AIDecision
    {
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private float checkRadius;
        [SerializeField] private CardManager cardManager;
        public override bool Decide()
        {
            if (cardManager.totalCardCount < 5) return false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, enemyMask);

            if (colliders.Length != 0)
            {
                if (_brain.Target == null)
                    _brain.Target = colliders[0].transform;
                return true;
            }
            else return false;
        }
    }
}
