using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    public Transform target;
    public float agroRange = 5f;
    private LineRenderer lineRenderer;
    //private bool isClicked = false;

    private PlayerMovement playerScript;
    public bool isConnected = false;

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
    }

    void Update()
    {
        if (target == null) return;

        float distToPlayer = Vector2.Distance(transform.position, target.position);
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
