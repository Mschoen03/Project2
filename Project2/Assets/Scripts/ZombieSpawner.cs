using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject regularZombiePrefab;
    [SerializeField] private GameObject fastZombiePrefab;
    [SerializeField] private GameObject tankZombiePrefab;

    [SerializeField] private int currentRound = 1;  // can be set from a game manager


    [Header("Spawning Control")]
    [SerializeField] private bool startSpawningImmediately = false;
    [SerializeField] private float spawnStartDelay = 30f;
    private bool spawningActive = false;

    [Header("Spawn Timing")]
    [SerializeField] private float _minimumSpawnTime = 5f;
    [SerializeField] private float _maximumSpawnTime = 8f;
    [SerializeField] private float minimumAllowedSpawnTime = 0.25f;
    [SerializeField] private float spawnSpeedRampRate = 0.1f;

    [Header("Spawn Limit")]
    [SerializeField] private int _maxSpawnCount = 50;

    private float _timeUntilSpawn;
    private int _spawnedCount = 0;

    private ZombieGameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<ZombieGameManager>();
        SetTimeUntilSpawn();

        if (startSpawningImmediately)
        {
            StartCoroutine(DelayedSpawnStart());
        }
    }

    public void SetRound(int round)
    {
        currentRound = round;
        _spawnedCount = 0; // 🔥 reset spawn counter for new round
    }


    public void StartSpawningManually(bool useDelay = false)
    {
        if (useDelay)
            StartCoroutine(DelayedSpawnStart());
        else
            EnableSpawning();
    }


    private IEnumerator DelayedSpawnStart()
    {
        yield return new WaitForSeconds(spawnStartDelay);
        EnableSpawning();
    }

    private void EnableSpawning()
    {
        if (spawningActive) return;

        spawningActive = true;
        Debug.Log($"{name} is now ACTIVE for spawning on round {currentRound}");
    }



    void Update()
    {
        if (!spawningActive || _spawnedCount >= _maxSpawnCount)
            return;

        _minimumSpawnTime = Mathf.Max(minimumAllowedSpawnTime, _minimumSpawnTime - spawnSpeedRampRate * Time.deltaTime);
        _maximumSpawnTime = Mathf.Max(_minimumSpawnTime + 0.1f, _maximumSpawnTime - spawnSpeedRampRate * Time.deltaTime);

        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            GameObject zombieToSpawn = regularZombiePrefab; // default

            // Decide based on round
            if (currentRound == 2 && Random.value < 0.2f)
                zombieToSpawn = fastZombiePrefab;

            else if (currentRound == 3 && Random.value < 0.4f)
                zombieToSpawn = fastZombiePrefab;

            else if (currentRound == 4)
                zombieToSpawn = Random.value < 0.2f ? tankZombiePrefab : (Random.value < 0.5f ? fastZombiePrefab : regularZombiePrefab);

            else if (currentRound == 5)
            {
                float roll = Random.value;
                if (roll < 0.3f) zombieToSpawn = fastZombiePrefab;
                else if (roll < 0.5f) zombieToSpawn = tankZombiePrefab;
                else zombieToSpawn = regularZombiePrefab;

                // Optionally increase spawn rate
                _minimumSpawnTime = 0.5f;
                _maximumSpawnTime = 1.5f;
            }

            Instantiate(zombieToSpawn, transform.position, Quaternion.identity);

            _spawnedCount++;

            FindObjectOfType<WaveController>()?.RegisterZombie();

            if (!spawningActive)
            {
                return;
            }
            else
            {
                Debug.Log($"{name} is active and checking spawn. Spawned so far: {_spawnedCount}/{_maxSpawnCount}");
            }



            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
