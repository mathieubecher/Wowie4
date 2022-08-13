using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_speed = 3.0f;
    [SerializeField] private bool m_rebound = false;
    [SerializeField] private LayerMask m_layerMask = 0;

    private Vector2 m_currentVelocity;

    private Rigidbody2D m_rigidbody;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody.velocity = transform.right * m_speed;
        m_currentVelocity = m_rigidbody.velocity;
    }

    // Update is called once per frame
    void Update()
    {}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(m_rebound && m_layerMask == (m_layerMask | 1 << collision.gameObject.layer))
        {
            Vector2 normal = collision.contacts[0].normal;
            m_currentVelocity = Vector2.Reflect(m_currentVelocity, normal);
            transform.position += (Vector3) m_currentVelocity.normalized * 0.1f;
            m_rigidbody.velocity = m_currentVelocity;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
