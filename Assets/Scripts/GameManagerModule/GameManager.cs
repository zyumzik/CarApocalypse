using System;
using AI;
using CameraModule;
using FiniteStateMachine;
using GameManagerModule.GameManagerStates;
using PlayerLogics;
using RoadSystem;
using UnityEngine;
using Zenject;
using StateMachine = FiniteStateMachine.StateMachine;

namespace GameManagerModule
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string _gameState = "None";
        
        [SerializeField] private Transform _playerSpawnPoint;
        
        [Inject] private Player _player;
        [Inject] private CameraController _cameraController;
        [Inject] private RoadConstructor _roadConstructor;
        [Inject] private RoadRaceHandler _roadRaceHandler;
        [Inject] private Enemy.Pool _enemyPool;

        private StateMachine _stateMachine;

        private Trigger _restartGameTrigger = new();
        private Trigger _startGameTrigger = new();
        private Trigger _playerDeathTrigger = new();
        private Trigger _raceFinishedTrigger = new();

        public event Action OnGameReady;
        public event Action OnGameStart;
        public event Action OnGameEnd;
        public event Action OnGameLose;
        public event Action OnGameWin;

        private void Start()
        {
            InitializeStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            _player.RegularHealth.OnDeath += PlayerOnDeath;
            _roadRaceHandler.OnFinished += OnRaceFinished;
        }

        private void OnDisable()
        {
            _player.RegularHealth.OnDeath -= PlayerOnDeath;
            _roadRaceHandler.OnFinished -= OnRaceFinished;
        }

        public void StartGame()
        {
            _startGameTrigger.Activate();
        }

        public void RestartGame()
        {
            _restartGameTrigger.Activate();
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new();
            _stateMachine.OnStateChanged += state => _gameState = state.GetType().ToString();
            
            // declaring state machine states
            var idleState = new IdleState(
                _player, _roadConstructor, _cameraController, _playerSpawnPoint, _roadRaceHandler, OnGameReady);
            var gameplayState = new GameplayState(_player, _cameraController, OnGameStart, OnGameEnd);
            var gameWinState = new GameWinState(OnGameWin);
            var gameLoseState = new GameLoseState(OnGameLose);
            
            // defining transitions
            _stateMachine.AddTransition(idleState, gameplayState, _startGameTrigger);
            _stateMachine.AddTransition(gameplayState, gameWinState, _raceFinishedTrigger);
            _stateMachine.AddTransition(gameplayState, gameLoseState, _playerDeathTrigger);
            _stateMachine.AddAnyTransition(idleState, _restartGameTrigger);
            
            // setting initial state
            _stateMachine.SetInitialState(idleState);
        }
        
        private void PlayerOnDeath()
        {
            _playerDeathTrigger.Activate();
        }
        
        
        private void OnRaceFinished()
        {
            _raceFinishedTrigger.Activate();
        }
    }
}