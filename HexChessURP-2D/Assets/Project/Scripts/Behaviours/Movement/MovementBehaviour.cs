using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBehaviour : Behaviour
{
    protected float movement_speed = 4f;
    protected float rotation_speed = 10f;
    public int range { get; set; }

    protected Tile current_tile = null;
    protected Tile next_tile = null;
    protected List<Tile> path { get; set; }

    public MovementBehaviour() : base() { }
    public MovementBehaviour(Unit _unit) : base(_unit)
    {
        path = new List<Tile>();
    }
    public MovementBehaviour(Unit _unit, int _range)
    {
        unit = _unit;
        range = _range;
        path = new List<Tile>();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Execute()
    {
        if (path.Count == 0)
        {
            Exit();
            return;
        }

        /*if (unit.IsDead())
        {
            Exit();
            return;
        }*/

        if (next_tile == null)
            next_tile = path[1];

        if ((next_tile.game_object.transform.position - unit.game_object.transform.position).sqrMagnitude <= 0.05f * 0.05f)
        {
            unit.game_object.transform.position = next_tile.game_object.transform.position;
            unit.game_object.transform.rotation = unit.target_rotation;

            current_tile.RemoveObject(unit);
            next_tile.AddObject(unit);

            //next_tile.TriggerModifier(unit);

            if (path.Count > 0)
                path.RemoveAt(0);

            if (path.Count > 0)
            {
                current_tile = next_tile;
                next_tile = path[0];
            }
        }
        else
        {
            unit.game_object.transform.position +=
                (next_tile.game_object.transform.position - unit.game_object.transform.position).normalized * movement_speed * Time.deltaTime;

            /* unit.target_rotation = Quaternion.LookRotation(Vector3.forward, (next_tile.game_object.transform.position - unit.game_object.transform.position).normalized);
             unit.game_object.transform.rotation = Quaternion.Slerp(unit.game_object.transform.rotation, unit.target_rotation, Time.deltaTime * rotation_speed);*/
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    public abstract List<Tile> GetAvailableMoves(Tile _unit_tile);
    public virtual void SetPath(Tile _unit_tile, Tile _desired_tile)
    {
        current_tile = _unit_tile;
        next_tile = null;
    }
}

