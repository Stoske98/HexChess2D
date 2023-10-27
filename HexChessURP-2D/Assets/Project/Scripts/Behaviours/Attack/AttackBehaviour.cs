using System.Collections.Generic;

public abstract class AttackBehaviour : Behaviour
{
    public int amount;
    protected int range;
    protected Damage damage;
    protected IDamagable target;
    public AttackBehaviour() : base() { }
    public AttackBehaviour(Unit _unit) : base(_unit) { }
    public AttackBehaviour(Unit _unit, Damage _damage, int _range) : base(_unit)
    {
        amount = _damage.amount;
        damage = _damage;
        range = _range;

        unit.game_object.GetComponentInChildren<HealthAndDamage>().SetDamageTxt(amount);
    }
    public virtual void SetAttack(IDamagable _target)
    {
        target = _target;
    }
    public virtual List<Tile> GetAttackMoves(Tile _unit_tile)
    {
        List<Tile> _attack_moves = new List<Tile>();

        List<Tile> _tiles = MapController.Instance.game.map.TilesInRange(_unit_tile, range);

        foreach (var _tile_in_range in _tiles)
        {
            Unit enemy = _tile_in_range.GetObjectOfType<Unit>();
            if (enemy != null && unit.class_type != enemy.class_type && enemy is IDamagable)
                _attack_moves.Add(_tile_in_range);
        }

        return _attack_moves;
    }

    public int GetAmount()
    {
        return amount;
    }
}

