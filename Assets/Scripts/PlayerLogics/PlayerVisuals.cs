using CombatSystem;
using GameManagerModule;
using UI;
using UnityEngine;
using Zenject;

namespace PlayerLogics
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private RegularHealth _playerHealth;
        [SerializeField] private GameObject _visuals;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private ParticleSystem[] _trailParticles;
        [SerializeField] private ParticleSystem _deathParticle;

        [Inject] private GameManager _gameManager;
        
        private void OnEnable()
        {
            _gameManager.OnGameReady += OnGameReady;
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
            _playerHealth.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            _gameManager.OnGameReady -= OnGameReady;
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
            _playerHealth.OnDeath -= OnDeath;
        }
        
        private void OnGameReady()
        {
            if (_deathParticle.isPlaying)
            {
                _deathParticle.gameObject.SetActive(false);
                _deathParticle.Stop();
            }

            if (!_visuals.gameObject.activeInHierarchy)
                _visuals.gameObject.SetActive(true);
        }
        
        private void OnGameStart()
        {
            if (!_healthBar.gameObject.activeInHierarchy)
                _healthBar.gameObject.SetActive(true);
            
            foreach (var particle in _trailParticles)
            {
                particle.Play();
            }
        }

        private void OnGameEnd()
        {
            foreach (var particle in _trailParticles)
            {
                particle.Stop();
            }
            
            _healthBar.gameObject.SetActive(false);
        }

        private void OnDeath()
        {
            _visuals.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            
            _deathParticle.gameObject.SetActive(true);
            _deathParticle.Play();
        }
    }
}