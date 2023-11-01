using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Behaviour/Attack/Ranged")]
public class RangedAttack_ScriptableObject : AttackBehaviour_ScriptableObject
{
    public GameObject ProjectilPrefab;

    public override void SetUp(Unit _unit)
    {
        Damage damage = null;

        switch (DamageType)
        {
            case TypeOfDamage.MAGIC:
                damage = new MagicDamage(_unit, Damage);
                break;
            case TypeOfDamage.PHYSICAL:
                damage = new PhysicalDamage(_unit, Damage);
                break;
            default:
                break;
        }

        _unit.AddBehaviour(new RangedAttack(_unit, damage, Range, AttackSpeed, ProjectilPrefab));
    }
}

