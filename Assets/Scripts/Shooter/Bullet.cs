using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_speed = 3.0f;
    [SerializeField] private bool m_rebound = false;
    [SerializeField] private LayerMask m_layerMask = 0;
    [SerializeField] private float m_lifeDistance = 10.0f;

    private float m_lifeTime = 0.0f;
    private Vector2 m_currentVelocity;

    private Rigidbody2D m_rigidbody;
    private HitBox m_hitbox;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_hitbox = GetComponent<HitBox>();
    }

    private void OnEnable()
    {
        m_hitbox.OnHurt += Hurt;
        m_hitbox.OnCollide += Hurt;
    }

    private void OnDisable()
    {
        
        m_hitbox.OnHurt -= Hurt;
        m_hitbox.OnCollide += Hurt;
    }

    private void Hurt(HurtBox _other)
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody.velocity = transform.right * (math.sign(transform.localScale.x) * m_speed);
        m_currentVelocity = m_rigidbody.velocity;

        m_lifeTime = m_lifeDistance / m_speed;

        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right * math.sign(transform.localScale.x), m_rigidbody.velocity);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(m_rebound && m_layerMask == (m_layerMask | 1 << collision.gameObject.layer))
        {
            Vector2 normal = collision.contacts[0].normal;
            m_currentVelocity = Vector2.Reflect(m_currentVelocity, normal);
            transform.position = collision.contacts[0].point + m_currentVelocity.normalized * 0.3f;
            m_rigidbody.velocity = m_currentVelocity;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(m_lifeTime);
        Destroy(gameObject);
    }
}
