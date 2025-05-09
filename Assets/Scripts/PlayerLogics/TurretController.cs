using Core.Interfaces;
using GameManagerModule;
using UnityEngine;
using Zenject;

namespace PlayerLogics
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private Transform _turret;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxRotation;
        
        [Inject] GameManager _gameManager;
        
        private IInput _input;
        private bool _isActive;

        public void Initialize(IInput input)
        {
            _input = input;
            _input.OnInput += OnInput;
        }

        private void OnEnable()
        {
            if (_input is not null)
                _input.OnInput += OnInput;

            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
        }
        
        private void OnDisable()
        {
            _input.OnInput -= OnInput;
            
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnInput(float value)
        {
            if (!_isActive) return;
            
            RotateTurret(value);
        }

        private void OnGameStart()
        {
            _isActive = true;
            _turret.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void OnGameEnd()
        {
            _isActive = false;
        }
        
        private void RotateTurret(float normalizedX)
        {
            var t = (normalizedX + 1f) / 2f;
            var targetAngle = Mathf.Lerp(-_maxRotation, _maxRotation, t);
            var targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            _turret.rotation = Quaternion.Lerp(_turret.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}