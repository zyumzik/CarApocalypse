using AI.EnemyStates;
using CombatSystem;
using Configurations;
using DetectionModule;
using FiniteStateMachine;
using PlayerLogics;
using UnityEngine;
using Zenject;

namespace AI
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private string _currentState;
        
        [SerializeField] private Detector<Player> _playerDetector;
        [SerializeField] private RegularHealth _regularHealth;
        [SerializeField] private EnemyAC _enemyAc;
        
        [Inject] private IMemoryPool _pool;
        
        public RegularHealth RegularHealth => _regularHealth;
        
        private EnemyConfiguration _configuration;
        private int _damage;
        private float _movementSpeed;
        private float _visionRadius;
        private Player _player;
        private StateMachine _stateMachine;
        private TimeTrigger _isHitTrigger;

        public void Initialize(EnemyConfiguration configuration)
        {
            _configuration = configuration;
            _regularHealth.Initialize(_configuration.MaxHealth);
            InitializeStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            _playerDetector.OnTargetEnter += OnPlayerDetected;
            _playerDetector.OnTargetExit += OnPlayerLost;
            _regularHealth.OnTakeDamage += OnTakeDamage;
            _regularHealth.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            _playerDetector.OnTargetEnter -= OnPlayerDetected;
            _playerDetector.OnTargetExit -= OnPlayerLost;
            _regularHealth.OnTakeDamage -= OnTakeDamage;
            _regularHealth.OnDeath -= OnDeath;
        }

        public void ReturnToPool()
        {
            _pool.Despawn(this);
        }
        
        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.OnStateChanged += (state) => _currentState = state.ToString();
            
            // declaring states
            var idleState = new IdleState();
            var chaseState = new ChaseState(
                _playerDetector, _regularHealth, _enemyAc, transform, _configuration.MovementSpeed, 
                _configuration.RotationSpeed, _configuration.Damage, _configuration.KillItselfDistance);
            var attackedState = new AttackedState(_enemyAc);
            
            // defining triggers
            _isHitTrigger = new TimeTrigger(_stateMachine, _configuration.IsHitDuration);
            
            // defining transitions
            _stateMachine.AddTransition(idleState, chaseState, 
                new Condition(() => 
                    _player != null && 
                    _player.RegularHealth.IsAlive()));
            _stateMachine.AddTransition(chaseState, idleState, 
                new Condition(() => 
                    _player == null || 
                    !_player.RegularHealth.IsAlive()));
            _stateMachine.AddAnyTransition(attackedState, 
                new Condition(() => _isHitTrigger));
            _stateMachine.AddTransition(attackedState, idleState, 
                new Condition(() => !_isHitTrigger));
            
            // setting initial state
            _stateMachine.SetInitialState(idleState);
        }

        private void OnPlayerDetected(Player player)
        {
            _player = player;
        }

        private void OnPlayerLost(Player player)
        {
            _player = null;
        }

        private void OnTakeDamage(int damage)
        {
            _isHitTrigger.Activate();
        }
        
        private void OnDeath()
        {
            _pool.Despawn(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public void OnDespawned()
        {
            _pool = null;
        }
        
        public class Pool : MonoMemoryPool<Enemy>
        {
            protected override void OnSpawned(Enemy enemy)
            {
                enemy.gameObject.SetActive(true);
            }

            protected override void OnDespawned(Enemy enemy)
            {
                enemy.gameObject.SetActive(false);
                enemy._regularHealth.ResetHealth();
                enemy._stateMachine.Reset();
            }
        }
    }
}