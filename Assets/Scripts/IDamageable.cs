public interface IDamageable
{
    public int Health { get; set; }

    public int MaxHealth { get; set; }

    public int Heal(int amount);

    public int Damage(int amount);

}
