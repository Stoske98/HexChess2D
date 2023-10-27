using System.Collections.Generic;

public class NoMovement : MovementBehaviour
{
    public NoMovement() : base() { }
    public NoMovement(Unit _unit) : base(_unit) { }
    public override void Execute()
    {
    }

    public override List<Tile> GetAvailableMoves(Tile _unit_tile)
    {
        return new List<Tile>();
    }
}

