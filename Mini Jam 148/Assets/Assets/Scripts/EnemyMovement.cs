using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float agroRange = 5f;
    public float movementSpeed = 2f;
    public Rigidbody2D rb;
    public Transform Target;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, Target.position);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }
    }
    
    private void ChasePlayer()
    {
        if (Target)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            moveDirection = direction;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        }
    }
   

    private void StopChasePlayer()
    {
        rb.velocity = new Vector2(0f, 0f);
    }
     
}
