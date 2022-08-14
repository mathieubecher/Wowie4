using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Condition/TargetCondition/TargetCondition", order = 1)]
public class TargetCondition : Condition
{
    protected List<Transform> m_targets = new List<Transform>();

    public virtual void SetTargets(List<Transform> _targets)
    {
        m_targets = _targets;
    }

    public virtual Transform GetBestTarget()
    {
        return m_targets[0];
    }

    public override bool Poll()
    {
        return m_targets.Count > 0;
    }
}
