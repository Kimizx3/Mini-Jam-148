using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDot : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDir;
    private Transform target;
    public float moveSpeed = 1f;
    //[SerializeField] float chaseRange = 2f;
    private float distanceToTarget = Mathf.Infinity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Goal").transform;
        
        

    }

    private void Update()
    {
        distanceToTarget = Vector2.Distance(target.position, transform.position);
        if (distanceToTarget > 0)
        {
            Chase();
            rb.mass = 10000000f;
        }

        if (distanceToTarget <= 0)
        {
            StopChase();
            rb.mass = 0.1f;
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
