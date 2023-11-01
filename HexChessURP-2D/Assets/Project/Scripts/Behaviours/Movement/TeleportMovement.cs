using System;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MovementBehaviour
{
    float animation_time = 0.35f;
    private GameObject telepor_vfx_prefab;
    public TeleportMovement() : base() { }
    public TeleportMovement(Unit _unit, int _range, float _speed, GameObject _effect_prefab, float _animation_time) : base(_unit, _range, _speed)
    {
        animation_time = _animation_time;
        telepor_vfx_prefab = _effect_prefab;
    }
    public override void Enter()
    {
        base.Enter();
        unit.game_object.LeanScale(Vector3.zero, animation_time).setDelay(0.25f);
        unit.game_object.LeanRotate(new Vector3(0,0,720), animation_time).setDelay(0.25f);

        GameObject teleport1 = GameObject.Instantiate(telepor_vfx_prefab, path[0].game_object.transform.position, Quaternion.identity);
        teleport1.LeanScale(Vector3.one, 0.25f).setOnComplete(ReduceSize(teleport1));
        GameObject teleport2 = GameObject.Instantiate(telepor_vfx_prefab, path[1].game_object.transform.position, Quaternion.identity);
        teleport2.LeanScale(Vector3.one, 0.25f).setDelay(0.75f).setOnComplete(ReduceSize(teleport2));
    }

    private Action ReduceSize(GameObject game_object)
    {
        return () =>
        {
            game_object.LeanScale(Vector3.zero, 0.25f).setDelay(0.5f);
            GameObject.Destroy(game_object, 0.75f);
        };
    }
    public override void Execute()
    {
        if (Time.time >= time + animation_time * 2)
        {

            path[0].RemoveObject(unit);
            path[1].AddObject(unit);
            unit.game_object.transform.position = path[1].game_object.transform.position;
            unit.game_object.LeanScale(Vector3.one, animation_time);
            unit.game_object.LeanRotate(new Vector3(0, 0, -720), animation_time);

            path.Clear();

            Exit();
        }
    }
    public override List<Tile> GetAvailableMoves(Tile _unit_tile)
    {
        List<Tile> _available_moves = new List<Tile>();

        foreach (Hex hex in MapController.Instance.game.map.TilesInRange(_unit_tile, range))
            if (hex.IsWalkable())
                _available_moves.Add(hex);

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

