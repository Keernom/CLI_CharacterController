using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class AiredState : State
    {
        public AiredState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            character.MovementDirection.y += Physics2D.gravity.y * Time.deltaTime;
            character.PlayerTransform.Translate(character.MovementDirection * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
