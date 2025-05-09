using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configurations/Enemy Configuration")]
    public class EnemyConfiguration : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        [SerializeField] private float _killItselfDistance;
        [SerializeField] private float _isHitDuration;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        
        public int MaxHealth => _maxHealth;
        public int Damage => _damage;
        public float KillItselfDistance => _killItselfDistance;
        public float IsHitDuration => _isHitDuration;
        public float MovementSpeed  => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
    }
}