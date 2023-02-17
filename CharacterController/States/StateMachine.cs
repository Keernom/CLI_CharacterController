using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; set; }

        public void Initialize(State startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
