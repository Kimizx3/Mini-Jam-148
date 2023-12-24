using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDot : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDir;
    private GameObject[] targets; // Changed to an array
    public float moveSpeed = 2f;
    [SerializeField] float chaseRange = 2f;
    private float distanceToTarget = Mathf.Infinity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("PinkDot"); // Changed to plural
    }

    private void Update()
    {
        // Iterate through all targets and find the closest one
        distanceToTarget = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance < distanceToTarget)
            {
                distanceToTarget = distance;
            }
        }

        if (distanceToTarget <= chaseRange)
        {
            Chase();
            rb.mass = 0.1f;
        }
        else
        {
            StopChase();
            rb.mass = 10000f;
        }
    }

    private void Chase()
    {
        GameObject closestTarget = FindClosestTarget();
        if (closestTarget)
        {
            Vector3 direction = (closestTarget.transform.position - transform.position).normalized;
            moveDir = direction;
            rb.velocity = new Vector2(moveDir.x, moveDir.y) * moveSpeed;
        }
    }

    private void StopChase()
    {
        rb.velocity = Vector2.zero;
    }

    private GameObject FindClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }
}

