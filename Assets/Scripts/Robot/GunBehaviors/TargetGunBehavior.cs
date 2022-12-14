using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/TargetGunBehavior", order = 4)]
public class TargetGunBehavior : GunBehavior
{
    [SerializeField] protected TargetCondition m_targetCondition;

    public override void Reset()
    {
        FindObjectOfType<Robot>().SetDistanceDetect(m_targetCondition.distance);
    }

    public override void UpdateTargets(List<Transform> _targets)
    {
        m_targetCondition.SetTargets(_targets);
    }

    public override bool CanShoot()
    {
        return m_condition.Poll() && m_targetCondition.Poll();
    }

    public override bool Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        if(m_targetCondition.Poll())
        {
            Transform target = m_targetCondition.GetBestTarget();
            Vector3 direction = target.transform.position + Vector3.up - _startPos;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            return m_gunType.Shoot(_startPos, angle, true, _bulletLayer);
        }

        return false;
    }
}
