using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Behaviour/Attack/Melee")]
public class MeleeAttack_ScriptableObject : AttackBehaviour_ScriptableObject
{
    public GameObject SlashPrefab;

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

        _unit.AddBehaviour(new MeleeAttack(_unit, damage, Range, AttackSpeed, SlashPrefab));

    }
}

