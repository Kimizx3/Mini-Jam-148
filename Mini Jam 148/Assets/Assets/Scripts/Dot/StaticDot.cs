using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDot : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDir;
    private Transform target;
    public float moveSpeed = 2f;
    [SerializeField] float chaseRange = 2f;
    private float distanceToTarget = Mathf.Infinity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PinkDot").transform;
        
    }

    private void Update()
    {
        distanceToTarget = Vector2.Distance(target.position, transform.position);
        
        if (distanceToTarget <= chaseRange)
        {
            Chase();
            rb.mass = 0.1f;
        }

        if (distanceToTarget > chaseRange)
        {
            StopChase();
            rb.mass = 10000f;
        }
    }

    private void Chase()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDir = direction;
            rb.velocity = new Vector2(moveDir.x, moveDir.y) * moveSpeed;
        }
    }
    
    private void StopChase()
    {
        rb.velocity = new Vector2(0f, 0f);
    }
}
