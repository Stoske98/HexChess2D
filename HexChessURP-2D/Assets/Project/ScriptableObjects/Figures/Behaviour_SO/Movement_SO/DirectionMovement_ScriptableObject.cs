using UnityEngine;

[CreateAssetMenu(fileName = "DirectionalMovement", menuName = "Behaviour/Movement/DirectionalMovement")]
public class DirectionMovement_ScriptableObject : Movement_ScriptableObject
{
    public override void SetUp(Unit _unit)
    {
        _unit.AddBehaviour(new DirectionMovement(_unit, Range, Speed));
    }

}

