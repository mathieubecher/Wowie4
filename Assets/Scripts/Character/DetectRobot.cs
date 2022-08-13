using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRobot : MonoBehaviour
{
    [SerializeField] private Animator m_fsm;


    private void Start()
    {
        
    }

    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        m_fsm.SetBool("detectRobot", true);
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer != LayerMask.NameToLayer("Robot")) return;
        m_fsm.SetBool("detectRobot", false);
    }
}
