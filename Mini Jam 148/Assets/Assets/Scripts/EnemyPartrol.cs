using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartrol : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed = 2;
    private int waypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
    }
}
