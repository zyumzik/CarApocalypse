namespace Core.Interfaces
{
    public interface IAttacker
    {
        int Damage { get; }
        
        void Attack(IDamageable damageable) { damageable.TakeDamage(Damage); }
    }
}