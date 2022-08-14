using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/AngularGunBehavior", order = 2)]
public class AngularGunBehavior : GunBehavior
{
    [SerializeField] private float m_angle = 45f;

    public override void Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        m_gunType.Shoot(_startPos, m_angle, _goRight, _bulletLayer);
    }
}
