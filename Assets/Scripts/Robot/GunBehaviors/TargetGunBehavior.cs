using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/TargetGunBehavior", order = 4)]
public class TargetGunBehavior : GunBehavior
{
    public override void Shoot(Vector3 _startPos, bool _goRight, List<Transform> _detectedTargets)
    {
        if(_detectedTargets.Count > 0)
        {
            Vector3 direction = _detectedTargets[0].transform.position - _startPos;
            float angle = Vector3.Angle(Vector3.right, direction);
            m_gunType.Shoot(_startPos, angle, true);
        }
    }
}
