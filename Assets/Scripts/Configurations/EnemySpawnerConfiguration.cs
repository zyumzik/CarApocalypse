using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "EnemySpawnerConfiguration", menuName = "Configurations/Enemy Spawner Configuration")]
    public class EnemySpawnerConfiguration : ScriptableObject
    {
        [Header("Timing")]
        [SerializeField] private AnimationCurve _densityCurve;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _noiseTimeScale;
        
        [Header("Spawn count")]
        [SerializeField] private int _minEnemiesPerWave;
        [SerializeField] private int _maxEnemiesPerWave;
        
        [Header("Positioning")]
        [SerializeField] private float _spawnOffsetZ;
        [SerializeField] private float _spawnWidth;
        [SerializeField] private float _zScatterRange;
        
        public AnimationCurve DensityCurve => _densityCurve;
        public float SpawnInterval => _spawnInterval;
        public float NoiseTimeScale => _noiseTimeScale;
        
        public int MinEnemiesPerWave => _minEnemiesPerWave;
        public int MaxEnemiesPerWave => _maxEnemiesPerWave;
        
        public float SpawnOffsetZ => _spawnOffsetZ;
        public float SpawnWidth => _spawnWidth;
        public float ZScatterRange => _zScatterRange;
    }
}