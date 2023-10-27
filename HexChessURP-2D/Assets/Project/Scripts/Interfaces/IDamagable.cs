public interface IDamagable
{
    int max_health { get; set; }
    int current_health { get; set; }
    void ReceiveDamage(Damage damage);
    void Die();
    bool IsAlive() { if (current_health > 0) return true; return false; }
}
