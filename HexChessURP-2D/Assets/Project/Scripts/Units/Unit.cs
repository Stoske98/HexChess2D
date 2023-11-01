using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : IObject
{
    public string id { get; set; }
    public Data data { get; set; }
    public ClassType class_type { get; set; }
    public UnitType unit_type { get; set; }
    public Visibility visibility { get; set; }
    public GameObject game_object { get; set; }
    public Quaternion target_rotation { set; get; }
    private List<Behaviour> behaviours { get; set; }
    private Queue<Behaviour> to_do_behaviours { get; set; }

    public Unit(ClassType _class_type, UnitType _unit_type, Data _data)
    {
        class_type = _class_type;
        unit_type = _unit_type;
        data = _data;
        behaviours = new List<Behaviour>();
        to_do_behaviours = new Queue<Behaviour>();
        game_object = GameObject.Instantiate(Resources.Load<GameObject>(_data.game_object_path));
        game_object.transform.SetParent(MapController.Instance.objects_containter.transform);
    }

    public Unit(Unit_ScriptableObject _unit_data)
    {
        class_type = _unit_data.ClassType;
        unit_type = _unit_data.UnitType;
        game_object = GameObject.Instantiate(_unit_data.MainPrefab);
        game_object.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _unit_data.MainSprite;
        game_object.transform.SetParent(MapController.Instance.objects_containter.transform);
        behaviours = new List<Behaviour>();
        to_do_behaviours = new Queue<Behaviour>();

        foreach (Behaviour_ScriptableObject behaviour_so in _unit_data.BehaviourScriptableObjects) 
            behaviour_so.SetUp(this);
    }
    public void Update()
    {
        if (to_do_behaviours.Count > 0)
            to_do_behaviours.Peek().Execute();
    }

    public void Attack(IDamagable _target)
    {
        AttackBehaviour attack_behaviour = GetBehaviour<AttackBehaviour>();
        attack_behaviour.SetAttack(_target);
        AddBehaviourToWork(attack_behaviour);
    }
    public void Move(Tile _unit_tile, Tile _desired_tile)
    {
        MovementBehaviour movement_behaviour = GetBehaviour<MovementBehaviour>();
        movement_behaviour.SetPath(_unit_tile, _desired_tile);
        AddBehaviourToWork(movement_behaviour);

    }
    public void AddBehaviour(Behaviour _behaviour)
    {
        behaviours.Add(_behaviour);
    }
    public void AddBehaviourToWork(Behaviour _behaviour)
    {
        if (_behaviour != null)
        {
            if (to_do_behaviours.Count == 0)
            {
                to_do_behaviours.Enqueue(_behaviour);
                _behaviour.Enter();
            }
            else
                to_do_behaviours.Enqueue(_behaviour);
        }
    }
    public T GetBehaviour<T>() where T : Behaviour
    {
        foreach (Behaviour behaviour in behaviours)
            if (behaviour is T t_behaviour)
                return t_behaviour;

        return null;
    }
    public void OnEndOfBehaviour()
    {
        if (to_do_behaviours.Count > 0)
            to_do_behaviours.Dequeue();

        if (to_do_behaviours.Count > 0)
            to_do_behaviours.Peek().Enter();
    }
    public bool IsWork()
    {
        return to_do_behaviours.Count > 0 ? true : false;
    }

    public void Dispose()
    {
        to_do_behaviours.Clear();
    }
}
