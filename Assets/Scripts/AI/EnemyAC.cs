using UnityEngine;

namespace AI
{
    public class EnemyAC : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _runningParameter = "IsRunning";
        [SerializeField] private string _hitParameter = "IsHit";

        private int _runningHash;
        private int _hitHash;
        
        private void Awake()
        {
            _runningHash = Animator.StringToHash(_runningParameter);
            _hitHash = Animator.StringToHash(_hitParameter);
        }

        public void SetRunning(bool value)
        {
            _animator.SetBool(_runningHash, value);
        }

        public void TriggerHit()
        {
            _animator.SetTrigger(_hitHash);
        }
    }
}