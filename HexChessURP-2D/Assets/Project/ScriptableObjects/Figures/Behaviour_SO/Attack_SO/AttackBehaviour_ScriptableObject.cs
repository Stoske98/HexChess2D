public abstract class AttackBehaviour_ScriptableObject : Behaviour_ScriptableObject
{
    public int Damage;
    public TypeOfDamage DamageType;
    public int Range;
    public float AttackSpeed;
}

public enum TypeOfDamage
{
    PHYSICAL,
    MAGIC,
}

