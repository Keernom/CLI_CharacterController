using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State
    {
        protected Character character;
        protected StateMachine stateMachine;
        protected LayerMask obstacleLayer;

        public State(Character character, StateMachine movementSM)
        {
            obstacleLayer = character.ObstacleLayer;
        }

        public virtual void Enter()
        {

        }        

        public virtual void StateUpdate()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}


