using System.Collections.Generic;

public class NormalMovement : MovementBehaviour
{
    public NormalMovement() : base() { }
    public NormalMovement(Unit _unit) : base(_unit) { }
    public NormalMovement(Unit _unit, int _range, float _speed) : base(_unit, _range, _speed)
    {
    }
    public override List<Tile> GetAvailableMoves(Tile _unit_tile)
    {
        List<Tile> _available_moves = new List<Tile>();

        foreach (Tile tile in PathFinding.BFS_HexesMoveRange(_unit_tile, range, MapController.Instance.game.map.tiles))
            if (tile.IsWalkable())
                _available_moves.Add(tile);

        return _available_moves;
    }
    public override void SetPath(Tile _unit_tile, Tile _desired_tile)
    {
        base.SetPath(_unit_tile, _desired_tile);

        path = PathFinding.FindPath_AStar(_unit_tile, _desired_tile, MapController.Instance.game.map.tiles);
    }
}

