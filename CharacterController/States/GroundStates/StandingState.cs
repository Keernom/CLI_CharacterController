using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StandingState : State
    {
        public StandingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
