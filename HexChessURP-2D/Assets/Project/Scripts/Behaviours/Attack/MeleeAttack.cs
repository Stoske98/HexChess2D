using System;
using UnityEngine;

public class MeleeAttack : AttackBehaviour
{
    GameObject slash_prefab;
    float speed = 6;
    float animation_speed = 0f;
    Tile _target_tile = null;
    Tile _unit_tile = null;
    public MeleeAttack() : base() { }
    public MeleeAttack(Unit _unit, Damage _damage, int _range) : base(_unit, _damage, _range)
    {
        if (_unit.class_type == ClassType.Light)
            slash_prefab = Resources.Load<GameObject>("Effects/Light/slash");
        else
            slash_prefab = Resources.Load<GameObject>("Effects/Dark/slash");
    }

    public override void Enter()
    {
        base.Enter();

        _target_tile = MapController.Instance.game.map.GetTile((Unit)target);
        _unit_tile = MapController.Instance.game.map.GetTile(unit);

        float distance = Vector3.Distance(_unit_tile.game_object.transform.position, _target_tile.game_object.transform.position);
        animation_speed = (distance / speed) * 2;

        unit.game_object.LeanScale(unit.game_object.transform.localScale * 2f, distance / speed).setOnComplete(Attack(distance));
    }

    private Action Attack(float distance)
    {
        return () =>
        {
            Vector3 direcition = (_target_tile.game_object.transform.position - _unit_tile.game_object.transform.position).normalized * 0.25f;
            unit.game_object.LeanMove(_target_tile.game_object.transform.position - direcition, distance / speed);
        };
    }

    public override void Execute()
    {
        if (Time.time >= time + animation_speed)
        {
            target.ReceiveDamage(new PhysicalDamage(unit, amount));
            GameObject.Destroy(GameObject.Instantiate(slash_prefab, _target_tile.game_object.transform.position, Quaternion.LookRotation(Vector3.forward, (_target_tile.game_object.transform.position - unit.game_object.transform.position).normalized)), 2);

            float distance = Vector3.Distance(_unit_tile.game_object.transform.position, _target_tile.game_object.transform.position);
            LTDescr tween = unit.game_object.LeanMove(_unit_tile.game_object.transform.position, distance / speed);
            if (_target_tile.IsWalkable())
            {
                _unit_tile.RemoveObject(unit);
                _target_tile.AddObject(unit);
                tween.setOnComplete(TakeTheField(distance));
            }
            unit.game_object.LeanScale(unit.game_object.transform.localScale / 2f, 0);
            target = null;
            Exit();
        }
    }


    private Action TakeTheField(float distance)
    {
        return () =>
        {
            unit.game_object.LeanMove(_target_tile.game_object.transform.position, distance / speed).setDelay(0.5f);
        };
    }
}

