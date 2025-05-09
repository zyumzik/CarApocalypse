using System.Collections.Generic;
using DetectionModule;
using GameManagerModule;
using PlayerLogics;
using UnityEngine;
using Zenject;

namespace AI
{
    public class EnemyDespawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyDetector _enemyDetector;
        [SerializeField] private float _offsetZ;

        [Inject] private GameManager _gameManager;
        [Inject] private Player _player;

        private void Update()
        {
            _enemyDetector.transform.position = 
                _player.transform.position + new Vector3(0f, 0, _offsetZ);
        }

        private void OnEnable()
        {
            _gameManager.OnGameEnd += OnGameEnd; 
            _enemyDetector.OnTargetEnter += OnEnemyEnter;
        }
        
        private void OnDisable()
        {
            _gameManager.OnGameEnd -= OnGameEnd;
            _enemyDetector.OnTargetEnter -= OnEnemyEnter;
        }

        private void OnGameEnd()
        {
            var spawnedEnemies = _enemySpawner.SpawnedEnemies;
            foreach (var enemy in spawnedEnemies)
            {
                if (enemy.gameObject.activeInHierarchy)
                    enemy.ReturnToPool();
            }
        }
        
        private void OnEnemyEnter(Enemy enemy)
        {
            enemy.ReturnToPool();
        }
    }
}