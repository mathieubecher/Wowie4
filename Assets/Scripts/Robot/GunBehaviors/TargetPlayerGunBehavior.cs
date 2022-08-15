using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/TargetPlayerGunBehavior", order = 5)]
public class TargetPlayerGunBehavior : GunBehavior
{
    public override bool Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        Transform player = FindObjectOfType<Character>().transform;
        Vector3 direction = player.transform.position + Vector3.up - _startPos;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        return m_gunType.Shoot(_startPos, angle, true, _bulletLayer);
    }
}
