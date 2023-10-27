using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MovementBehaviour
{
    public KnightMovement() : base() { }
    public KnightMovement(Unit _unit, int _range) : base(_unit, _range)
    {
    }
    public override List<Tile> GetAvailableMoves(Tile _unit_tile)
    {
        List<Tile> _available_moves = new List<Tile>();
        Map map = MapController.Instance.game.map;

        foreach (Tile _tile in map.TilesInRange(_unit_tile, range))
            if (_tile.IsWalkable() && (Mathf.Abs(_tile.coordinate.x - _unit_tile.coordinate.x) == range
                || Mathf.Abs(_tile.coordinate.y - _unit_tile.coordinate.y) == range
                || Mathf.Abs(((Hex)_tile).S - ((Hex)_unit_tile).S) == range))
                _available_moves.Add(_tile);

        return _available_moves;
    }
    public override void SetPath(Tile _unit_tile, Tile _desired_tile)
    {
        base.SetPath(_unit_tile, _desired_tile);

        path.Clear();
        path.Add(_unit_tile);
        path.Add(_desired_tile);
    }
}

