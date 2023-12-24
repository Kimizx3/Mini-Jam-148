using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PushDotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pushDotPrefab;
    public float minimumSpawnTime = 5f;
    public float maximumSpawnTime = 10f;

    private float timeUntilSpawn;
    private bool hasStartedSpawning = false;

    private void Awake()
    {
        // Start the countdown after 60 seconds
        StartCoroutine(StartSpawningAfterDelay(60f));
    }

    private IEnumerator StartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasStartedSpawning = true;
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        if (hasStartedSpawning)
        {
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= 0)
            {
                SpawnPushDot();
                SetTimeUntilSpawn();
            }
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void SpawnPushDot()
    {
        Instantiate(pushDotPrefab, transform.position, Quaternion.identity);
    }
}

