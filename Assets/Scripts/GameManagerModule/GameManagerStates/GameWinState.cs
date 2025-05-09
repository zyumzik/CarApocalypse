using System;
using PlayerLogics;
using RoadSystem;

namespace GameManagerModule.GameManagerStates
{
    public class GameWinState : GameEndState
    {
        public GameWinState(Action callback) : base(callback)
        {
        }
    }
}