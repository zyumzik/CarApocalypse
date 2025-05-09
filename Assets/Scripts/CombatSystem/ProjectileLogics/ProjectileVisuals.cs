using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CombatSystem.ProjectileSystem
{
    public class ProjectileVisuals : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        private async void OnDisable()
        {
            try
            {
                if (!_trailRenderer) return;

                _trailRenderer.enabled = false;
                await UniTask.Yield();
                _trailRenderer.Clear();
                _trailRenderer.enabled = true;
            }
            catch (Exception e) {}
        }
    }
}