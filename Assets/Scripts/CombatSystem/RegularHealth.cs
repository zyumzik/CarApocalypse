using System;
using Core.Interfaces;
using UnityEngine;

namespace CombatSystem
{
    public class RegularHealth : MonoBehaviour, IHealth, IDamageable
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        
        private bool _isDead;
        
        public event Action<int> OnTakeDamage;
        public event Action OnDeath;

        public void Initialize (int maxHealth)
        {
            CurrentHealth = MaxHealth = maxHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            _isDead = false;
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        public void TakeDamage(int damage)
        {
            if (!IsAlive()) return;
            
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            if (CurrentHealth == 0 && !_isDead)
            {
                _isDead = true;
                OnDeath?.Invoke();
            }
            else 
                OnTakeDamage?.Invoke(damage);
        }

        public void Kill()
        {
            TakeDamage(CurrentHealth);
        }
    }
}