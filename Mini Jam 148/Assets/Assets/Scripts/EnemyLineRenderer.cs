using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    public Transform target;
    public float agroRange = 5f;
    private LineRenderer lineRenderer;
    private bool isClicked = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (target == null) return;

        float distToPlayer = Vector2.Distance(transform.position, target.position);
        if (distToPlayer < agroRange && isClicked)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
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
}
