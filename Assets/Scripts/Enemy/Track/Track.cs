using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Track : MonoBehaviour
{
    [SerializeField] private bool m_loop = true;
    private List<Vector2> m_positions;
    private int m_currentPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        EdgeCollider2D edgeCollider;
        if (TryGetComponent<EdgeCollider2D>(out edgeCollider))
        {
            Vector2[] points = edgeCollider.points;
            m_positions = new List<Vector2>();
            foreach (var point in points)
            {
                m_positions.Add((Vector2)transform.position + point);
            }

            Destroy(edgeCollider);
        }
    }

    public Vector2 GetVelocity(Vector2 _position, float _speed)
    {
        if(m_positions.Count < 2) return Vector2.zero;
        
        Vector2 direction = m_positions[m_currentPoint] - _position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance < _speed * Time.deltaTime)
        {
            ++m_currentPoint;
            m_currentPoint = m_currentPoint >= m_positions.Count ? 0 : m_currentPoint;
            direction = m_positions[m_currentPoint] - _position;
            distance = direction.magnitude;
            direction.Normalize();
        }

        return direction * _speed;
    }
    
    private void OnDrawGizmos()
    {
        if (m_positions != null)
        {
            Vector3 startPos = Vector3.zero;
            foreach (var position in m_positions)
            {
                if (startPos != Vector3.zero)
                {
                    Debug.DrawLine(startPos, position, Color.cyan);
                }
                startPos = position;
            }
        }
        
    }
}
