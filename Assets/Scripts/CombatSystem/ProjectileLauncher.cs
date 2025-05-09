using System;
using System.Collections.Generic;
using System.Threading;
using CombatSystem.ProjectileSystem;
using Configurations;
using GameManagerModule;
using TimerService;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace CombatSystem
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private ProjectileConfiguration _projectileConfiguration;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _initialPoolSize;
        
        [Inject] private DiContainer _container;
        [Inject] private GameManager _gameManager;
        [Inject] private ProjectileController _projectileController;
        
        private ObjectPool<Projectile> _projectilePool;
        private HashSet<Projectile> _activeProjectiles = new();
        
        private float _spawnInterval;
        private float _timer;
        private bool _isActive;
        
        public event Action OnProjectileLaunched;

        public void Initialize(float spawnInterval)
        {
            _spawnInterval = spawnInterval;
        }
        
        private void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>(
                createFunc: () =>
                {
                     var projectile = _container.InstantiatePrefabForComponent<Projectile>(
                        _projectileConfiguration.ProjectilePrefab);
                     projectile.Initialize(_projectileConfiguration.Damage, _projectileConfiguration.ProjectileSpeed, 
                         _projectileConfiguration.LifeTime, destroyCallback: 
                         () => _projectilePool.Release(projectile));
                     return projectile;
                },
                actionOnGet: projectile =>
                {
                    projectile.gameObject.SetActive(true);
                    _activeProjectiles.Add(projectile);
                    _projectileController.AddProjectile(projectile);
                },
                actionOnRelease: projectile =>
                {
                    projectile.gameObject.SetActive(false);
                    _activeProjectiles.Remove(projectile);
                    _projectileController.RemoveProjectile(projectile);
                },
                actionOnDestroy: projectile =>
                {
                    _projectileController.RemoveProjectile(projectile);
                    Destroy(projectile.gameObject);
                }, 
                false, _initialPoolSize);
        }

        private void Update()
        {
            if (!_isActive) return;
            
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer -= _spawnInterval;
                SpawnProjectile();
            }
        }

        private void OnEnable()
        {
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameStart()
        {
            _isActive = true;

            if (_activeProjectiles.Count > 0)
            {
                foreach (var projectile in  _activeProjectiles)
                {
                    _projectilePool.Release(projectile);
                }
            }
        }

        private void OnGameEnd()
        {
            _isActive = false;
        }
        
        private void SpawnProjectile()
        {
            var projectile = _projectilePool.Get();
            projectile.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
            projectile.Launch();
            
            OnProjectileLaunched?.Invoke();
        }
    }
}