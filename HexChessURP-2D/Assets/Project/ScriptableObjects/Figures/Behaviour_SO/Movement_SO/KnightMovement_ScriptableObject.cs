using UnityEngine;

[CreateAssetMenu(fileName = "KnightMovement", menuName = "Behaviour/Movement/KnightMovement")]
public class KnightMovement_ScriptableObject : Movement_ScriptableObject
{
    public override void SetUp(Unit _unit)
    {
        _unit.AddBehaviour(new KnightMovement(_unit, Range, Speed));
    }

}

