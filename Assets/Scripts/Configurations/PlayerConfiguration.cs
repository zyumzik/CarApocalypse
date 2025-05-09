using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configurations/Player Configuration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _shootInterval;
        
        public int MaxHealth => _maxHealth;
        public float MovementSpeed  => _movementSpeed;
        public float ShootInterval => _shootInterval;
    }
}