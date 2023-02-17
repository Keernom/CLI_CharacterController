using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class GroundedState : State
    {
        public GroundedState(Character character, StateMachine movementSM) : base(character, movementSM)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            
            if (!character.DownSideCheck(obstacleLayer))
            {
                character.JumpsRemain--;
                stateMachine.ChangeState(character.Falling);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
