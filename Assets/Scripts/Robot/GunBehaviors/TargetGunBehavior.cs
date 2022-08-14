using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/TargetGunBehavior", order = 4)]
public class TargetGunBehavior : GunBehavior
{
    [SerializeField] protected bool m_closest = true;
    [SerializeField] protected bool m_robotCentric = true;

    private Transform m_target;

    public override bool FindTarget(List<Transform> _detectedTargets)
    {
        if(_detectedTargets.Count > 0)
        {
            m_target = _detectedTargets[0];
            return true;
        }
        return false;
    }

    public override void Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        Vector3 direction = m_target.transform.position - _startPos;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        m_gunType.Shoot(_startPos, angle, true, _bulletLayer);
    }
}
