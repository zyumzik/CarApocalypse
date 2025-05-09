using System.Collections.Generic;
using Configurations;
using GameManagerModule;
using PlayerLogics;
using RoadSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AI
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerConfiguration _spawnerConfiguration;
        
        [Inject] private GameManager _gameManager;
        [Inject] private Player _player;
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private RoadRaceHandler _roadRaceHandler;

        public HashSet<Enemy> SpawnedEnemies => _spawnedEnemies;
        
        private readonly HashSet<Enemy> _spawnedEnemies = new();
        private bool _isActive;
        private float _timer;

        private void Update()
        {
            if (!_isActive) return;
            
            _timer += Time.deltaTime;
            if (_timer >= _spawnerConfiguration.SpawnInterval)
            {
                _timer -= _spawnerConfiguration.SpawnInterval;
                SpawnEnemy();
            }
        }

        private void OnEnable()
        {
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameWin += OnGameResult;
            _gameManager.OnGameLose += OnGameResult;
        }

        private void OnDisable()
        {
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameWin -= OnGameResult;
            _gameManager.OnGameLose -= OnGameResult;
        }
        
        private void OnGameStart()
        {
            _isActive = true;
            _spawnedEnemies.Clear();
        }

        private void OnGameResult()
        {
            _isActive = false;
        }
        
        private void SpawnEnemy()
        {
            float playerZ = _player.transform.position.z;
            float baseZ = playerZ + _spawnerConfiguration.SpawnOffsetZ;
            float spawnWidth = _spawnerConfiguration.SpawnWidth;
            float scatterZ = _spawnerConfiguration.ZScatterRange;

            float playerDistance = _roadRaceHandler.CurrentDistance;
            float densityFactor = _spawnerConfiguration.DensityCurve.Evaluate(playerDistance);

            int min = Mathf.RoundToInt(_spawnerConfiguration.MinEnemiesPerWave * densityFactor);
            int max = Mathf.RoundToInt(_spawnerConfiguration.MaxEnemiesPerWave * densityFactor);
            int enemiesToSpawn = Random.Range(min, max + 1);

            float timeOffset = Time.time * _spawnerConfiguration.NoiseTimeScale;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                float noise = Mathf.PerlinNoise(i * 0.3f, timeOffset);
                float x = Mathf.Lerp(-spawnWidth / 2f, spawnWidth / 2f, noise);

                float z = baseZ + Random.Range(0f, scatterZ);

                Vector3 position = new Vector3(x, 0, z);
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                Enemy enemy = _enemyFactory.Create(position, rotation);
                _spawnedEnemies.Add(enemy);
            }
        }


    }
}