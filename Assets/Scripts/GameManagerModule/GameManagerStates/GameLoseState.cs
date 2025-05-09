using System;
using PlayerLogics;
using RoadSystem;

namespace GameManagerModule.GameManagerStates
{
    public class GameLoseState : GameEndState
    {
        public GameLoseState(Action callback) : base(callback)
        {
        }
    }
}