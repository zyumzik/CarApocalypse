using System;

namespace Core.Interfaces
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }

        bool IsAlive();

        event Action OnDeath;
    }
}