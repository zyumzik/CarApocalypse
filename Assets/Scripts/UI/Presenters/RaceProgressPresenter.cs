using System;
using BufferSystem;
using GameManagerModule;
using RoadSystem;
using Zenject;
using Object = UnityEngine.Object;

namespace UI.Presenters
{
    public class RaceProgressPresenter : ITickable
    {
        private readonly GameManager _gameManager;
        private RoadRaceHandler _roadRaceHandler;
        
        private readonly RaceProgressView _raceProgressView;

        public RaceProgressPresenter(string rootUIId, BufferManager bufferManager, GameManager gameManager, RoadRaceHandler roadRaceHandler)
        {
            _gameManager = gameManager;
            _roadRaceHandler = roadRaceHandler;
            var rootUI = bufferManager.GetObject<RootUI>(rootUIId);
            
            _raceProgressView = rootUI.RaceProgressView;
            _gameManager.OnGameStart += GameManagerOnGameStart;
            _gameManager.OnGameEnd += GameManagerOnGameEnd;
        }

        private void GameManagerOnGameStart()
        {
            _raceProgressView.Show();
        }

        private void GameManagerOnGameEnd()
        {
            _raceProgressView.Hide();
        }

        public void Tick()
        {
            _raceProgressView?.SetProgress(_roadRaceHandler.CurrentProgress);
        }
    }
}