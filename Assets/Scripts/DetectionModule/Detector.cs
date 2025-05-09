using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Managers;
using TimerService;
using UnityEngine;
using Zenject;

namespace DetectionModule
{
    [RequireComponent(typeof(Collider))]
    public class Detector<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private LayerMask _overlapMask;
        [SerializeField] private UpdateType _updateType = UpdateType.FixedTick;
        [SerializeField] private int _overlapCacheSize = 16;
                                                       
        [Inject] private TicksManager _ticksManager;

        public HashSet<T> Targets { get; private set; } = new();
        public List<T> CurrentTargets = new();

        private Collider[] _overlapResults;
        private CancellationTokenSource _cts;

        public event Action<T> OnTargetEnter;
        public event Action<T> OnTargetExit;
        
        private void Awake()
        {
            _overlapResults = new Collider[_overlapCacheSize];
        }

        private void OnEnable()
        {
            switch (_updateType)
            {
                case UpdateType.Tick:
                    _ticksManager.OnTick += UpdateDetector;
                    break;
                case UpdateType.FixedTick:
                    _ticksManager.OnFixedTick += UpdateDetector;
                    break;
                case UpdateType.LateTick:
                    _ticksManager.OnLateTick += UpdateDetector;
                    break;
            }
        }

        private void OnDisable()
        {
            switch (_updateType)
            {
                case UpdateType.Tick:
                    _ticksManager.OnTick -= UpdateDetector;
                    break;
                case UpdateType.FixedTick:
                    _ticksManager.OnFixedTick -= UpdateDetector;
                    break;
                case UpdateType.LateTick:
                    _ticksManager.OnLateTick -= UpdateDetector;
                    break;
            }
        }

        public bool TryGetTarget(out T target)
        {
            if (Targets.Count > 0)
            {
                target = Targets.FirstOrDefault();
                return target != null;
            }

            target = null;
            return false;
        }

        public void ForceDetect() => UpdateDetector();

        private void UpdateDetector()
        {
            var newTargets = Detect();
            UpdateTargets(newTargets);
        }
        
        private HashSet<T> Detect()
        {
            var targets = new HashSet<T>();
            var count = 0;
            
            if (_collider is SphereCollider sphereCollider)
            {
                var radius = sphereCollider.radius * Mathf.Max(
                    transform.lossyScale.x,
                    transform.lossyScale.y,
                    transform.lossyScale.z);
                count = Physics.OverlapSphereNonAlloc(
                    transform.position, radius, _overlapResults, _overlapMask);
            }
            else if (_collider is BoxCollider boxCollider)
            {
                var center = boxCollider.transform.TransformPoint(boxCollider.center);
                var halfExtents = boxCollider.size * 0.5f;
                var orientation = boxCollider.transform.rotation;
                count = Physics.OverlapBoxNonAlloc(
                    center, halfExtents, _overlapResults, orientation, _overlapMask);
            }

            for (int i = 0; i < count; i++)
            {
                var collider = _overlapResults[i];
                if (collider is not null 
                    && collider.gameObject.activeInHierarchy                     
                    && collider.TryGetComponent<T>(out var target))
                {
                    targets.Add(target);
                }
            }
            
            return targets;
        }

        private void UpdateTargets(HashSet<T> detectedTargets)
        {
            Targets.RemoveWhere(t => t == null);
            
            var added = detectedTargets.Except(Targets).ToHashSet();
            foreach (var target in added)
            {
                OnTargetEnter?.Invoke(target);
            }
            
            var removed = Targets.Except(detectedTargets).ToHashSet();
            foreach (var target in removed)
            {
                OnTargetExit?.Invoke(target);
            }
            
            Targets.UnionWith(added);
            Targets.ExceptWith(removed);
            CurrentTargets = new List<T>(Targets);
        }

        private enum UpdateType
        {
            Tick,
            FixedTick,
            LateTick,
        }
    }
}