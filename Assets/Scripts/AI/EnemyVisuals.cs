using CombatSystem;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI
{
    public class EnemyVisuals : MonoBehaviour
    {
        [SerializeField] private RegularHealth _regularHealth;
        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Transform _objectToScale;
        [SerializeField] private Vector3 _punchForce = new(0.5f, 0.5f, 0.5f);
        [SerializeField] private int _punchVibrato = 3;
        [SerializeField] private float _punchElasticity = 1.0f;
        [SerializeField] private float _punchDuration = 1f;
        [SerializeField] private Color _damageColor = Color.white;
        [SerializeField] private ParticleSystem _deathParticlePrefab;

        private Material _material;
        private Color _originalColor;
        private Tween _takeDamageTween;

        private void Awake()
        {
            _material = _mesh.material;
            _originalColor = _material.color;
        }

        private void OnEnable()
        {
            _regularHealth.OnTakeDamage += OnTakeDamage;
            _regularHealth.OnDeath += OnDeath;
            
            _healthBar.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _regularHealth.OnTakeDamage -= OnTakeDamage;
            _regularHealth.OnDeath -= OnDeath;
        }

        private void OnTakeDamage(int damage)
        {
            TakeDamageAnimation();
        }

        private void TakeDamageAnimation()
        {
            // Kill previous if still playing
            _takeDamageTween?.Kill();

            var sequence = DOTween.Sequence();

            // Scale punch
            sequence.Join(_objectToScale.DOPunchScale(
                _punchForce, _punchDuration, _punchVibrato, _punchElasticity));

            // Color to red and back
            sequence.Join(_material.DOColor(_damageColor, _punchDuration / 2f));
            sequence.Append(_material.DOColor(_originalColor, _punchDuration / 2f));

            _takeDamageTween = sequence;
        }
        
        private void OnDeath()
        {
            _takeDamageTween?.Complete();
            _healthBar.gameObject.SetActive(false);
            
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}