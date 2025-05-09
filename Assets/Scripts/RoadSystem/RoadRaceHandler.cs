using System;
using Configurations;
using PlayerLogics;
using UnityEngine;
using Zenject;

namespace RoadSystem
{
    public class RoadRaceHandler : MonoBehaviour
    {
        [SerializeField] private RaceConfiguration _raceConfiguration;
        [SerializeField] private Transform _startRacePoint;
        
        public float CurrentDistance { get; private set; }
        public float CurrentProgress { get; private set; }
        
        [Inject] private Player _player;

        private bool _isFinished;
        
        public event Action OnFinished;

        private void Update()
        {
            if (_isFinished) return;
            
            CurrentDistance = Vector3.Distance(_player.transform.position, _startRacePoint.position);
            CurrentProgress = CurrentDistance / _raceConfiguration.RaceDistance;
            
            if (CurrentDistance >= _raceConfiguration.RaceDistance)
            {
                _isFinished = true;
                OnFinished?.Invoke();
            }
        }

        public void ResetDistance()
        {
            CurrentDistance = 0;
            CurrentProgress = 0;
            _isFinished = false;
        }
    }
}