using System.Collections.Generic;

public class NoAttack : AttackBehaviour
{
    public NoAttack() : base() { }
    public NoAttack(Unit _unit) : base(_unit)
    {
        unit.game_object.GetComponentInChildren<HealthAndDamage>().SetDamageTxt(0);
    }
    public override void Execute()
    {
    }

    public override List<Tile> GetAttackMoves(Tile _tile)
    {
        return new List<Tile>();
    }
}

