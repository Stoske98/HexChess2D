using UnityEngine;

[CreateAssetMenu(fileName = "TeleportMovement", menuName = "Behaviour/Movement/TeleportMovement")]
public class TeleportMovement_ScriptableObject : Movement_ScriptableObject
{
    public GameObject EffectPrefab;
    public float TeleportAnimationTime;
    public override void SetUp(Unit _unit)
    {
        _unit.AddBehaviour(new TeleportMovement(_unit, Range, Speed, EffectPrefab, TeleportAnimationTime));
    }

}

