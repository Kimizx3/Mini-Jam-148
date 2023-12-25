using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    //Player Movement [Horizontal + Vertical]
    [Header("Movement Setting")]
    public float baseMoveSpeed = 5f;  // Base movement speed
    private float currentMoveSpeed;   // Current movement speed
    private const float speedReductionPerEnemy = 0.15f; // 15% speed reduction per enemy
    private int maxSpeedReductionCount = 6; // Maximum number of enemies affecting speed
    public Rigidbody2D rb;


    [SerializeField] private AudioClip collisionSound; // AudioClip to play on collision
    private AudioSource audioSource; // AudioSource component
    private Dictionary<GameObject, float> cooldownTimers = new Dictionary<GameObject, float>();
    private const float cooldownDuration = 0.5f; // Cooldown duration in seconds

    [SerializeField] private AudioClip wallCollisionSound; // AudioClip for Wall collisions



    //Vector2 movement;
    Vector2 moveDirection;
    //Dash
    /*
    [Header("Dash Setting")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 12f;
    public float dahsingTime = 0.2f;
    public float dashingCooldown = 1f;
    */
    //Disable Link
    private bool isLinked;
    
    public float growSpeed = 0.25f;
    //public float cameraCoolDown = 3f;
    public DistanceJoint2D dj;
    
    
    [SerializeField] private TrailRenderer tr;
    //Count Slider
    [SerializeField] private CounterUI _counterUI;


    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed; // Initialize current speed to base speed
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the GameObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        /*
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        */
        
        
        DisableLink();
        
        

        moveDirection = new Vector2(moveX, moveY).normalized;
        //move speed test
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //moveSpeed = 2f;
        //}
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            //connectionCost(1);
            Debug.Log(GameManager.gameManager.numConn.Connection);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            //restoreConnection(1);
            Debug.Log(GameManager.gameManager.numConn.Connection);
        }
        */
        // Update cooldown timers
        var keys = new List<GameObject>(cooldownTimers.Keys);
        foreach (var key in keys)
        {
            cooldownTimers[key] -= Time.deltaTime;
            if (cooldownTimers[key] <= 0)
            {
                cooldownTimers.Remove(key);
            }
        }

    }

    private void FixedUpdate()
    {
        if (isLinked)
        {
            return;
        }
        /*
        if (isDashing)
        {
            return;
        }
        */
        // Adjust player speed based on number of connections
        int connections = Mathf.Min(connectedEnemies.Count, maxSpeedReductionCount);
        currentMoveSpeed = baseMoveSpeed * (1 - connections * speedReductionPerEnemy);
        currentMoveSpeed = Mathf.Max(currentMoveSpeed, 0); // Ensure speed doesn't go negative
        rb.MovePosition(rb.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);

        //if (isDashing)
        //{
            //return;
        //}
        //rb.MovePosition(rb.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);
    }

    /*
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
    */

    private void DisableLink()
    {
        if (dj.distance <= 2f)
        {
            isLinked = true;
            dj.distance += growSpeed * Time.deltaTime;
        }
        if (dj.distance > 2f)
        {
            isLinked = false;
            dj.enabled = false;
        }
    }
    
    //private void PlayerMovement

    /*
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
    */
    
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




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "StaticDot" || collision.gameObject.tag == "PushDot") && CanPlayAudio(collision.gameObject))
        {
            PlayCollisionSound(collision.gameObject, collisionSound);
        }
        else if (collision.gameObject.tag == "Wall" && CanPlayAudio(collision.gameObject))
        {
            PlayCollisionSound(collision.gameObject, wallCollisionSound);
        }
    }

    private void PlayCollisionSound(GameObject collidedObject, AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
        cooldownTimers[collidedObject] = cooldownDuration; // Set cooldown for this object
    }

    private bool CanPlayAudio(GameObject collidedObject)
    {
        if (cooldownTimers.ContainsKey(collidedObject))
        {
            if (cooldownTimers[collidedObject] > 0)
            {
                return false; // Still in cooldown
            }
        }
        return true; // No cooldown or cooldown has ended
    }
}
