using DetectionModule;
using GameManagerModule;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

namespace PlayerLogics
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private SplineAnimate _splineAnimate;
        [SerializeField] private RoadDetector _roadDetector;

        [Inject] private GameManager _gameManager;
        
        public void Initialize(float movementSpeed)
        {
            _splineAnimate.MaxSpeed = movementSpeed;
        }

        private void OnEnable()
        {
            _gameManager.OnGameReady += OnGameReady;
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
            _roadDetector.OnTargetEnter += OnRoadEnter;
        }

        private void OnDisable()
        {
            _gameManager.OnGameReady -= OnGameReady;
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
            _roadDetector.OnTargetEnter -= OnRoadEnter;
        }

        public void StartMovement()
        {
            _splineAnimate.enabled = true;
            _splineAnimate.Restart(true);
        }

        public void StopMovement()
        {
            _splineAnimate.Pause();
            _splineAnimate.enabled = false;
        }

        private void OnGameReady()
        {
            StopMovement();
        }
        
        private void OnGameStart()
        {
            StartMovement();
        }

        private void OnGameEnd()
        {
            StopMovement();
        }
        
        private void OnRoadEnter(RoadSegment segment)
        {
            _splineAnimate.Container = segment.Spline;
            
            if (_splineAnimate.enabled) 
                _splineAnimate.Restart(true);
        }
    }
}