using System.Collections.Generic;

public class DirectionMovement : MovementBehaviour
{
    public DirectionMovement() : base() { }
    public DirectionMovement(Unit _unit, int _range) : base(_unit)
    {
        range = _range;
    }

    public override List<Tile> GetAvailableMoves(Tile _unit_tile)
    {
        List<Tile> _available_moves = new List<Tile>();
        Map map = MapController.Instance.game.map;

        _available_moves.AddRange(map.GetTilesInDirection(Direction.UP, _unit_tile, range, false));
        _available_moves.AddRange(map.GetTilesInDirection(Direction.DOWN, _unit_tile, range, false));
        _available_moves.AddRange(map.GetTilesInDirection(Direction.LOWER_RIGHT, _unit_tile, range, false));
        _available_moves.AddRange(map.GetTilesInDirection(Direction.UPPER_RIGHT, _unit_tile, range, false));
        _available_moves.AddRange(map.GetTilesInDirection(Direction.LOWER_LEFT, _unit_tile, range, false));
        _available_moves.AddRange(map.GetTilesInDirection(Direction.UPPER_LEFT, _unit_tile, range, false));

        return _available_moves;
    }
    public override void SetPath(Tile _unit_tile, Tile _desired_tile)
    {
        base.SetPath(_unit_tile, _desired_tile);

        path = PathFinding.FindPath_AStar(_unit_tile, _desired_tile, MapController.Instance.game.map.tiles);
    }
}

