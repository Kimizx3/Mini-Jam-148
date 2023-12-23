using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    public Transform target;
    public float agroRange = 5f;
    private LineRenderer lineRenderer;

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
        if (distToPlayer < agroRange)
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
}
