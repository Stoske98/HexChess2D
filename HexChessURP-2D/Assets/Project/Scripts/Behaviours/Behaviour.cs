using UnityEngine;

public abstract class Behaviour
{
    protected Unit unit { get; set; }
    protected float time { get; set; }
    public Behaviour() : base() { }
    public Behaviour(Unit _unit)
    {
        unit = _unit;
    }
    public virtual void Enter()
    {
        time = Time.time;
    }
    public abstract void Execute();
    public virtual void Exit()
    {
        unit.OnEndOfBehaviour();
    }
}

