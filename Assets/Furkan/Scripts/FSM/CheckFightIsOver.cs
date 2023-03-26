using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class CheckFightIsOver : AIDecision
    {
        public bool fightIsOver = true;
        public override bool Decide()
        {
            return fightIsOver;
        }

       
    }
}
