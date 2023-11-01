using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Unit", menuName = "Unit/Figure")]
public abstract class Unit_ScriptableObject : ScriptableObject
{
    public string Name;
    public UnitType UnitType;
    public ClassType ClassType;
    public GameObject MainPrefab;
    public Sprite MainSprite;
    //public Sprite MainSprite;
    public List<Behaviour_ScriptableObject> BehaviourScriptableObjects;
}

