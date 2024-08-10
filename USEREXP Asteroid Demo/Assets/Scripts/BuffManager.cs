using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

// BuffManager is Main Buff is parent class for the buff instances(polymorhpism).  
public abstract class BuffManager : ScriptableObject
{
    public abstract Buff GetBuff(Character target);
}

public abstract class Buff : ScriptableObject
{
    public abstract void Apply(GameObject target);
}




