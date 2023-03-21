using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public abstract class AIAction : MonoBehaviour
    {
        public BaseStateMachine machine;

        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        
    }
}
