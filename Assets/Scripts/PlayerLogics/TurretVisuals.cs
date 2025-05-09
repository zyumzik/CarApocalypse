using CombatSystem;
using DG.Tweening;
using GameManagerModule;
using UnityEngine;
using Zenject;

namespace PlayerLogics
{
    public class TurretVisuals : MonoBehaviour
    {
        [SerializeField] private ProjectileLauncher _projectileLauncher;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _objectToScale;
        [SerializeField] private float _punchDuration = 0.25f;
        [SerializeField] private Vector3 _punchForce = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private int _punchVibrato = 2;
        [SerializeField] private float _punchElasticity = 1.0f;

        [Inject] private GameManager _gameManager;
        
        private Tween _tween;
        
        private void OnEnable()
        {
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
            _projectileLauncher.OnProjectileLaunched += OnProjectileLaunched;
        }

        private void OnDisable()
        {
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
            _projectileLauncher.OnProjectileLaunched -= OnProjectileLaunched;
        }

        private void OnGameStart()
        {
            _lineRenderer.enabled = true;
        }

        private void OnGameEnd()
        {
            _lineRenderer.enabled = false;
        }
        
        private void OnProjectileLaunched()
        {
            _tween?.Complete();
            
            _tween = _objectToScale.DOPunchScale(_punchForce, _punchDuration, _punchVibrato, _punchElasticity);
        }
    }
}