using Configurations;
using UnityEngine;

namespace AI
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly Enemy.Pool _enemyPool;
        private readonly EnemyConfiguration _enemyConfiguration;

        public EnemyFactory(Enemy.Pool enemyPool, EnemyConfiguration enemyConfiguration)
        {
            _enemyPool = enemyPool;
            _enemyConfiguration = enemyConfiguration;
        }

        public Enemy Create(Vector3 position, Quaternion rotation)
        {
            var enemy = _enemyPool.Spawn();
            enemy.Initialize(_enemyConfiguration);
            enemy.transform.SetPositionAndRotation(position, rotation);
            return enemy;
        }
    }
}