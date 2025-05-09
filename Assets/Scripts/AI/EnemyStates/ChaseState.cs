using CombatSystem;
using DetectionModule;
using FiniteStateMachine;
using PlayerLogics;
using UnityEngine;

namespace AI.EnemyStates
{
    public class ChaseState : State
    {
        private readonly Detector<Player> _playerDetector;
        private readonly RegularHealth _regularHealth;
        private readonly EnemyAC _enemyAc;
        private readonly Transform _transform;
        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;
        private readonly int _damage;
        private readonly float _killItselfDistance;

        public ChaseState(Detector<Player> playerDetector, RegularHealth regularHealth, EnemyAC enemyAc, 
            Transform transform, float movementSpeed, float rotationSpeed, int damage, float killItselfDistance)
        {
            _playerDetector = playerDetector;
            _regularHealth = regularHealth;
            _enemyAc = enemyAc;
            _transform = transform;
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
            _damage = damage;
            _killItselfDistance = killItselfDistance;
        }

        public override void Enter()
        {
            _enemyAc.SetRunning(true);
        }

        public override void Update()
        {
            if (!_playerDetector.TryGetTarget(out var player)) return;

            if (Vector3.Distance(_transform.position, player.transform.position) <= _killItselfDistance)
            {
                player.RegularHealth.TakeDamage(_damage);
                _regularHealth.Kill();
            }
            
            var directionToPlayer = player.transform.position - _transform.position;
            directionToPlayer.y = 0;
            
            var targetVelocity = directionToPlayer.normalized * _movementSpeed;
            _transform.Translate(targetVelocity * Time.deltaTime, Space.World);
            
            var targetRotation = Quaternion.LookRotation(directionToPlayer);
            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation, 
                targetRotation, 
                _rotationSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            _enemyAc.SetRunning(false);
        }
    }
}