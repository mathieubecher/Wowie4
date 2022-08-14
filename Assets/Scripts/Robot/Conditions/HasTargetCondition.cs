using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Condition/HasTargetCondition", order = 2)]
public class HasTargetCondition : Condition
{
    [SerializeField] private DetectTargets m_detectTargets;

    public override bool Poll()
    {
        return m_detectTargets.targets.Count > 0;
    }
}
