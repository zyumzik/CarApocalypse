using System;
using CameraModule;
using FiniteStateMachine;
using PlayerLogics;

namespace GameManagerModule.GameManagerStates
{
    public class GameplayState : State
    {
        private readonly Player _player;
        private readonly CameraController _cameraController;
        private readonly Action _onGameStart;
        private readonly Action _onGameEnd;

        public GameplayState(Player player, CameraController cameraController, Action onGameStart, Action onGameEnd)
        {
            _player = player;
            _cameraController = cameraController;
            _onGameStart = onGameStart;
            _onGameEnd = onGameEnd;
        }

        public override void Enter()
        {
            _cameraController.SwitchCamera(CameraController.CameraType.Gameplay);
            _cameraController.SetCameraTarget(CameraController.CameraType.Gameplay, _player.CameraLookPoint);
            _onGameStart?.Invoke();
        }

        public override void Exit()
        {
            _onGameEnd?.Invoke();
        }
    }
}