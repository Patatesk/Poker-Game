using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace PK.PokerGame
{
    public class CheckDistance : AIDecision
    {
        [SerializeField] private float distance;

        public override bool Decide()
        {
            if (Vector3.Distance(transform.position, _brain.Target.position) > distance) return false;
            else return true;
        }
    }
}
