using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    //Player Movement [Horizontal + Vertical]
    [Header("Movement Setting")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    //Vector2 movement;
    Vector2 moveDirection;
    //Dash
    [Header("Dash Setting")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 12f;
    public float dahsingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    //Count Slider
    [SerializeField] private CounterUI _counterUI;

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        //move speed test
        //if (Input.GetKeyDown(KeyCode.T))
        //{
            //moveSpeed = 2f;
        //}
        if (Input.GetKeyDown(KeyCode.X))
        {
            connectionCost(1);
            Debug.Log(GameManager.gameManager.numConn.Connection);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            restoreConnection(1);
            Debug.Log(GameManager.gameManager.numConn.Connection);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashingPower, moveDirection.y *dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dahsingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void connectionCost(int cost)
    {
        GameManager.gameManager.numConn.ConnectionCost(cost);
        _counterUI.setCounter(GameManager.gameManager.numConn.Connection);
        
    }

    private void restoreConnection(int restore)
    {
        GameManager.gameManager.numConn.ConnectionRestore(restore);
        _counterUI.setCounter(GameManager.gameManager.numConn.Connection);
    }
    //player connect Enemies count
    private List<GameObject> connectedEnemies = new List<GameObject>();
    private const int maxConnections = 6;

    public bool AddConnection(GameObject enemy)
    {
        if (connectedEnemies.Count < maxConnections)
        {
            connectedEnemies.Add(enemy);
            return true; // Connection successful
        }
        return false; // Connection failed
    }

    public void RemoveConnection(GameObject enemy)
    {
        if (connectedEnemies.Contains(enemy))
        {
            connectedEnemies.Remove(enemy);
        }
    }

    // Use this to check if an enemy is already connected
    public bool IsEnemyConnected(GameObject enemy)
    {
        return connectedEnemies.Contains(enemy);
    }
}
