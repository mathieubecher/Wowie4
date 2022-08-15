using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform m_goTo;
    private void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("Enter");
        Teleportable teleportable = _other.GetComponentInParent<Teleportable>();
        Debug.Log(teleportable);
        if (teleportable)
        {
            Debug.Log("Enter Success");
            teleportable.transform.position += m_goTo.transform.position - transform.position;
        }
    }
}
