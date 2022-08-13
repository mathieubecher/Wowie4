using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPhysics : MonoBehaviour
{
    private List<Transform> m_contactGrounds;
    public bool isOnGround => m_contactGrounds.Count > 0;

    public delegate void HitRoofDelegate();
    public event HitRoofDelegate OnHitRoof;
    private void Awake()
    {
        m_contactGrounds = new List<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        bool added = false;
        foreach (var contact in other.contacts)
        {
            if (!added && Vector2.Dot(contact.normal, Vector2.up) > 0.7f)
            {
                m_contactGrounds.Add(other.transform);
                added = true;
            }
            else if (other.gameObject.layer != LayerMask.NameToLayer("Platform") && Vector2.Dot(contact.normal, Vector2.down) > 0.7f)
            {
                OnHitRoof?.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        m_contactGrounds.Remove(other.transform);
    }
}
