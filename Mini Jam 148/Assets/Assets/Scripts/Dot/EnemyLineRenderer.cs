using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    public Transform target;
    public float agroRange = 5f;
    private LineRenderer lineRenderer;
    //private bool isClicked = false;

    private PlayerMovement playerScript;
    public bool isConnected = false;

    public List<Transform> pinkDots; // List to store multiple Pink Dots

    public Transform closestPinkDot; // Set this in Update when finding the closest Pink Dot
    private bool shouldConnectToPlayer;


    public Transform GetChaseTarget() 
    { 
        return isConnected ? (shouldConnectToPlayer ? target : closestPinkDot) : null; 
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerMovement>();
        }

        target = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        pinkDots = new List<Transform>();
        foreach (GameObject pinkDot in GameObject.FindGameObjectsWithTag("Pink"))
        {
            pinkDots.Add(pinkDot.transform);
            //Debug.Log(pinkDot.transform);
            //Debug.Log(pinkDots);
        }
    }

    void Update()
    {
        if (target == null || pinkDots == null || pinkDots.Count == 0) return;
        float distToPlayer = Vector2.Distance(transform.position, target.position);
        float distToPinkDot = float.MaxValue; // Declare this variable here
        Transform closestPinkDot = null;
        float closestPinkDotDistance = float.MaxValue;

        // Find the closest Pink Dot
        foreach (Transform pinkDot in pinkDots)
        {
            distToPinkDot = Vector2.Distance(transform.position, pinkDot.position);
            if (distToPinkDot < closestPinkDotDistance)
            {
                closestPinkDotDistance = distToPinkDot;
                closestPinkDot = pinkDot;
            }
        }

        shouldConnectToPlayer = distToPlayer < closestPinkDotDistance;

        if (shouldConnectToPlayer)
        {
            if (!isConnected && distToPlayer < agroRange && playerScript.AddConnection(gameObject))
            {
                isConnected = true;
            }
        }
        else // Connecting to Pink Dot
        {
            if (!isConnected && closestPinkDotDistance < agroRange && closestPinkDot.GetComponent<PinkDot2>().AddConnection(gameObject))
            {
                isConnected = true;
            }
        }

        if (isConnected)
        {
            lineRenderer.enabled = true;
            Vector3 targetPosition = shouldConnectToPlayer ? target.position : closestPinkDot.position;
            lineRenderer.SetPosition(0, transform.position); // Enemy's current position
            lineRenderer.SetPosition(1, targetPosition); // Target's current position
        }
        else if (distToPlayer >= agroRange && distToPinkDot >= agroRange)
        {
            lineRenderer.enabled = false;
            if (shouldConnectToPlayer)
            {
                playerScript.RemoveConnection(gameObject);
            }
            else
            {
                closestPinkDot.GetComponent<PinkDot2>().RemoveConnection(gameObject);
            }
            isConnected = false;
        }
        /*
        if (distToPlayer < agroRange)
        {
            if (!isConnected && playerScript.AddConnection(gameObject))
            {
                isConnected = true;
            }

            if (isConnected)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position); // Enemy's current position
                lineRenderer.SetPosition(1, target.position); // Player's current position
            }
        }
        else if (isConnected && distToPlayer >= agroRange)
        {
            lineRenderer.enabled = false;
            playerScript.RemoveConnection(gameObject);
            isConnected = false;
        }
        */
    }

    /*
    private void OnMouseDown()
    {
        // Check if the mouse is over the sprite when clicked
        if (Vector2.Distance(target.position, transform.position) <= agroRange)
        {
            isClicked = true;
        }
    }

    // Optionally, reset isClicked when not drawing the line
    private void OnMouseUp()
    {
        isClicked = false;
    }
        */

    public bool IsLineActive()
    {
        return lineRenderer.enabled;
    }

}
