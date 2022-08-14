using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/TargetGunBehavior", order = 4)]
public class TargetGunBehavior : GunBehavior
{
    [SerializeField] protected bool m_closest = true;
    [SerializeField] protected bool m_robotCentric = true;

    private Enemy m_target;

    public override bool FindTarget(List<Enemy> _detectedEnemies)
    {
        if(_detectedEnemies.Count > 0)
        {
            m_target = _detectedEnemies[0];
            return true;
        }
        return false;
    }

    public override void Shoot(Vector3 _startPos, bool _goRight)
    {
        Vector3 direction = m_target.transform.position - _startPos;
        float angle = Vector3.Angle(Vector3.right, direction);
        m_gunType.Shoot(_startPos, angle, true);
    }
}
