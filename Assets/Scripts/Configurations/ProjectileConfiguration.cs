using CombatSystem.ProjectileSystem;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "ProjectileConfiguration", menuName = "Configurations/Projectile Configuration")]
    public class ProjectileConfiguration : ScriptableObject
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _lifeTime;
        
        public Projectile ProjectilePrefab => _projectilePrefab;
        public int Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
        public float LifeTime => _lifeTime;
    }
}