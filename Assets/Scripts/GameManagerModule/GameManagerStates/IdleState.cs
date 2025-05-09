using System;
using CameraModule;
using FiniteStateMachine;
using PlayerLogics;
using RoadSystem;
using UnityEngine;

namespace GameManagerModule.GameManagerStates
{
    public class IdleState : State
    {
        private readonly Player _player;
        private readonly RoadConstructor _roadConstructor;
        private readonly CameraController _cameraController;
        private readonly Transform _playerSpawnPoint;
        private readonly RoadRaceHandler _roadRaceHandler;
        private readonly Action _enterCallback;

        public IdleState(Player player, RoadConstructor roadConstructor,
            CameraController cameraController, Transform playerSpawnPoint, 
            RoadRaceHandler roadRaceHandler, Action enterCallback)
        {
            _player = player;
            _roadConstructor = roadConstructor;
            _cameraController = cameraController;
            _playerSpawnPoint = playerSpawnPoint;
            _roadRaceHandler = roadRaceHandler;
            _enterCallback = enterCallback;
        }

        public override void Enter()
        {
            _roadConstructor.ConstructRoad();
            _roadRaceHandler.ResetDistance();
            _player.transform.SetPositionAndRotation(_playerSpawnPoint.position, _playerSpawnPoint.rotation);
            _player.Respawn();
            _cameraController.SwitchCamera(CameraController.CameraType.Intro);
            _cameraController.SetCameraTarget(CameraController.CameraType.Intro, _player.CameraLookPoint);
            _enterCallback?.Invoke();
        }
    }
}