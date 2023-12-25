using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PushDotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pushDotPrefab;
    [SerializeField] private AudioClip spawnSound; // AudioClip to play on spawn

    private AudioSource audioSource; // AudioSource component
    public float minimumSpawnTime = 5f;
    public float maximumSpawnTime = 10f;

    private float timeUntilSpawn;
    private bool hasStartedSpawning = false;

    private void Awake()
    {
        // Start the countdown after 60 seconds
        StartCoroutine(StartSpawningAfterDelay(60f));
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the GameObject");
        }
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
        PlaySpawnSound();
    }
    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}

