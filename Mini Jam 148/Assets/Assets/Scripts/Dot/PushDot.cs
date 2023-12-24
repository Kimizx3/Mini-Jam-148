using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDot : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private float speed = 2f;
    private Vector2 moveDir;
    private float distanceToTarget = Mathf.Infinity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        distanceToTarget = Vector2.Distance(target.position, transform.position);
        
        if (distanceToTarget <= 0.5f)
        {
            ChasePlayer();
        }

        if (distanceToTarget > 2f)
        {
            StopChase();
        }
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDir = direction;
            rb.velocity = new Vector2(moveDir.x, moveDir.y) * speed;
        }
    }
    private void StopChase()
    {
        rb.velocity = new Vector2(0f, 0f);
    }
}
