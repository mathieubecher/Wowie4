using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/AngularGunBehavior", order = 2)]
public class AngularGunBehavior : GunBehavior
{
    [SerializeField] private List<float> m_angles;

    private int m_currentIndex = 0;

    public override void Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        if(m_currentIndex > m_angles.Count - 1)
        {
            m_currentIndex = 0;
        }

        if(m_gunType.Shoot(_startPos, m_angles[m_currentIndex], _goRight, _bulletLayer))
        {
            m_currentIndex++;
        }
    }
}
