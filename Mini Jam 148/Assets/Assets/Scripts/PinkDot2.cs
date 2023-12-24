using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkDot2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
