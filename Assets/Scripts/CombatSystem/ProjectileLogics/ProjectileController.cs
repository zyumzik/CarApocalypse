using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Managers;
using Zenject;

namespace CombatSystem.ProjectileSystem
{
    public class ProjectileController
    {
        private DiContainer _container;
        private readonly TicksManager _ticksManager;

        private readonly HashSet<IProjectile> _projectiles;
        
        public ProjectileController(TicksManager ticksManager)
        {
            _ticksManager = ticksManager;

            _projectiles = new HashSet<IProjectile>();
            
            _ticksManager.OnTick += OnTick; 
        }

        public void AddProjectile(IProjectile projectile)
        {
            _projectiles.Add(projectile);
        }

        public void RemoveProjectile(IProjectile projectile)
        {
            _projectiles.Remove(projectile);
        }

        private void OnTick()
        {
            foreach (var projectile in _projectiles.ToList())
            {
                projectile?.Move();
            }
        }
    }
}