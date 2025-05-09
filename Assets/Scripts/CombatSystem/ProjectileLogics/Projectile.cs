using System;
using AI;
using Core.Interfaces;
using Core.Managers;
using DetectionModule;
using UnityEngine;
using Zenject;

namespace CombatSystem.ProjectileSystem
{
    public class Projectile : MonoBehaviour, IProjectile, IAttacker
    {
        [SerializeField] private EnemyDetector _enemyDetector;

        [Inject] private TicksManager _ticksManager;
        
        public int Damage { get; private set; }
        public float Speed { get; private set; }
        public float LifeTime { get; private set; }

        private bool _isActive;
        private float _lifeTimer;
        private Action _destroyCallback;

        public void Initialize(int damage, float speed, float lifeTime, Action destroyCallback = null)
        {
            Damage = damage;
            Speed = speed;
            LifeTime = lifeTime;
            _destroyCallback = destroyCallback;
        }

        private void OnEnable()
        {
            _enemyDetector.OnTargetEnter += OnEnemyEnter;
        }
        
        private void OnDisable()
        {
            _enemyDetector.OnTargetEnter -= OnEnemyEnter;
            Stop();
        }

        public void Launch()
        {
            _isActive = true;
            _lifeTimer = LifeTime;
        }
        
        public void Move()
        {
            if (!_isActive) return;
            
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0)
            {
                _destroyCallback?.Invoke();
            }
            
            //_enemyDetector.ForceDetect();
            transform.position += transform.forward * (Speed * Time.deltaTime);
        }

        public void Stop()
        {
            _isActive = false;
        }
        
        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(Damage);
        }
        
        private void OnEnemyEnter(Enemy enemy)
        {
            if (enemy.TryGetComponent<IDamageable>(out var damageable))
            {
                Attack(damageable);
                Stop();
                _destroyCallback?.Invoke();
            }
        }
    }
}