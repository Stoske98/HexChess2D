using UnityEngine;

[CreateAssetMenu(fileName = "NormalMovement", menuName = "Behaviour/Movement/NormalMovement")]
public class NormalMovement_ScriptableObject : Movement_ScriptableObject
{
    public override void SetUp(Unit _unit)
    {
        _unit.AddBehaviour(new NormalMovement(_unit, Range, Speed));
    }

}

