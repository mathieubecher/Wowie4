using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Condition/Condition", order = 1)]
public class Condition : ScriptableObject
{
    public virtual bool Poll()
    {
        return true;
    }
}
