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
    private EnemyLineRenderer lineRendererScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        lineRendererScript = GetComponent<EnemyLineRenderer>();
    }

    private void Update()
    {
        Transform chaseTarget = lineRendererScript.GetChaseTarget();

        if (chaseTarget != null)
        {
            //Debug.Log(chaseTarget);
            ChaseTarget(chaseTarget);
        }
        else
        {
            StopChasePlayer();
        }
        /*
        float distToPlayer = Vector2.Distance(transform.position, Target.position);


        if (lineRendererScript.isConnected && Target)
        {
            // Regular chasing behavior
            ChasePlayer();
        }
        else if (distToPlayer < agroRange)
        {
            // Slower chasing behavior
            ChasePlayerSlowly();
        }
        else
        {
            // Stop chasing
            StopChasePlayer();
        }
        */
    }

    private void ChaseTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        moveDirection = direction;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
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

    private void ChasePlayerSlowly()
    {
        if (Target)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            moveDirection = direction;
            float slowSpeed = movementSpeed * 0.5f; // Reduce the speed to half, adjust as needed
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * slowSpeed;
        }
    }

}
