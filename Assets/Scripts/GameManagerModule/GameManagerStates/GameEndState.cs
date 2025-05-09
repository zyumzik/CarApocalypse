using System;
using FiniteStateMachine;

namespace GameManagerModule.GameManagerStates
{
    public class GameEndState : State
    {
        private readonly Action _callback;

        protected GameEndState(Action callback)
        {
            _callback = callback;
        }

        public override void Enter()
        {
            _callback?.Invoke();
        }
    }
}