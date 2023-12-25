using System.Collections;
using UnityEngine;

public class EndBlue : MonoBehaviour
{
    [SerializeField] private GameObject endBluePrefab;
    private float timeUntilSpawn;
    private bool hasStartedSpawning = false;
    private bool hasSpawned = false;

    [SerializeField] private float minimumSpawnTime = 5f;
    [SerializeField] private float maximumSpawnTime = 10f;

    private void Awake()
    {
        // Start the countdown after 60 seconds
        StartCoroutine(StartSpawningAfterDelay(65f));
    }

    private IEnumerator StartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasStartedSpawning = true;
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        if (hasStartedSpawning && !hasSpawned)
        {
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= 0)
            {
                SpawnEndBlue();
                hasSpawned = true; // Set the flag to true after spawning
            }
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void SpawnEndBlue()
    {
        if (endBluePrefab != null)
        {
            Instantiate(endBluePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab not assigned to EndBlue script.");
        }
    }
}

