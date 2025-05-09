using System;

namespace Core.Interfaces
{
    public interface IDamageable
    {
        public event Action<int> OnTakeDamage;

        void TakeDamage(int damage);
    }
}