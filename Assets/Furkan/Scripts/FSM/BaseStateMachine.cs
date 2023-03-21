using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace PK.PokerGame
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        protected AIAction currentState;


        public virtual void Update()
        {
            currentState?.Execute();
        }

        public virtual void ChangeState(AIAction newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.machine= this;
            currentState.Enter();
        } 


    }
    [System.Serializable]
    public class StatesAndDecions
    {
        public AIAction StateToPerform;
        

    }
}
