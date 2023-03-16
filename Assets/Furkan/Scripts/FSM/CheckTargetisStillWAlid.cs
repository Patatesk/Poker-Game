using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace PK.PokerGame
{
    public class CheckTargetisStillWAlid : AIDecision
    {
        public override bool Decide()
        {
           
            if (_brain.Target.gameObject.activeSelf) return true;
            else
            {
                _brain.Target = null;
                return false;
            }
        }

       
    }
}
