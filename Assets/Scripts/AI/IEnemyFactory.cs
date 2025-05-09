using UnityEngine;

namespace AI
{
    public interface IEnemyFactory
    {
        public Enemy Create(Vector3 position, Quaternion rotation);
    }
}