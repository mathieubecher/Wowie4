using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTargets : MonoBehaviour
{
    [SerializeField] private LayerMask m_targetLayers;
    [SerializeField] private List<Transform> m_targets;
    public List<Transform> targets => m_targets;
    
    private void Start()
    {
        m_targets = new List<Transform>();
    }

    private void Update()
    {}

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || (m_targetLayers.value & (1 << _other.gameObject.layer)) == 0) return;
        m_targets.Add(_other.transform);
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || (m_targetLayers.value & (1 << _other.gameObject.layer)) == 0) return;
        m_targets.Remove(_other.transform);
    }
}
