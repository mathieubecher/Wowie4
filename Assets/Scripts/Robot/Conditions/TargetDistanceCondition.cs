using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Condition/TargetCondition/TargetDistanceCondition", order = 2)]
public class TargetDistanceCondition : TargetCondition
{
    [SerializeField] protected bool m_closest = true;
    [SerializeField] protected bool m_robotCentric = true;
    [SerializeField] protected LayerMask m_layer;

    private Transform m_bestTarget;

    public override Transform GetBestTarget()
    {
        return m_bestTarget;
    }

    public override bool Poll()
    {
        if(m_targets.Count > 0)
        {
            Debug.Log("TargetDistanceCondition");
            Transform from = m_robotCentric ? FindObjectOfType<Robot>().transform : FindObjectOfType<Character>().transform;

            float bestDistance = -1f;
            foreach(Transform target in m_targets)
            {
                if((m_layer.value & (1 << target.gameObject.layer)) == 0) continue;
                Vector3 direction = from.position - target.position;
                float currentDistance = direction.magnitude;
                bool closest = m_closest ? bestDistance > currentDistance : bestDistance <= currentDistance;
                if(bestDistance == -1f || closest)
                {
                    bestDistance = currentDistance;
                    m_bestTarget = target;
                }
            }
            return bestDistance != -1f;
        }
        return false;
        
    }
}
