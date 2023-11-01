using UnityEngine;

public class RangedAttack : AttackBehaviour
{
    GameObject projectil_prefab;
    float arrow_fly_time = 0;
    public RangedAttack() : base() { }
    public RangedAttack(Unit _unit, Damage _damage, int _range, float _attack_speed, GameObject _projectil) : base(_unit, _damage, _range, _attack_speed)
    {
        projectil_prefab = _projectil;
    }
    public override void Enter()
    {
        base.Enter();
        GameObject arrow = GameObject.Instantiate(projectil_prefab, unit.game_object.transform.position, Quaternion.LookRotation(Vector3.forward, (((Unit)target).game_object.transform.position - unit.game_object.transform.position).normalized));
        float distance = Vector3.Distance(unit.game_object.transform.position, ((Unit)target).game_object.transform.position);
        arrow_fly_time = distance / attack_speed;
        Vector3 direcition = (((Unit)target).game_object.transform.position  - unit.game_object.transform.position).normalized * 0.25f;
        arrow.LeanMove(((Unit)target).game_object.transform.position - direcition, arrow_fly_time).setDestroyOnComplete(true);
    }
    public override void Execute()
    {
        if (Time.time >= time + arrow_fly_time)
        {
            target.ReceiveDamage(new PhysicalDamage(unit, amount));
            target = null;
            Exit();
        }
    }
}

