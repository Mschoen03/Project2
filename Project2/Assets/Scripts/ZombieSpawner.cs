using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject regularZombiePrefab;
    [SerializeField] private GameObject fastZombiePrefab;
    [SerializeField] private GameObject tankZombiePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float _minimumSpawnTime = 5f;
    [SerializeField] private float _maximumSpawnTime = 8f;
    [SerializeField] private float minimumAllowedSpawnTime = 0.25f;
    [SerializeField] private float spawnSpeedRampRate = 0.1f;

    [Header("Spawning Control")]
    [SerializeField] private int _maxSpawnCount = 50;

    private int currentRound = 1;
    private float _timeUntilSpawn;
    private int _spawnedCount = 0;
    private bool spawningActive = false;

    void Update()
    {
        if (!spawningActive || _spawnedCount >= _maxSpawnCount)
            return;

        _minimumSpawnTime = Mathf.Max(minimumAllowedSpawnTime, _minimumSpawnTime - spawnSpeedRampRate * Time.deltaTime);
        _maximumSpawnTime = Mathf.Max(_minimumSpawnTime + 0.1f, _maximumSpawnTime - spawnSpeedRampRate * Time.deltaTime);

        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            GameObject zombieToSpawn = regularZombiePrefab;

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

                _minimumSpawnTime = 0.5f;
                _maximumSpawnTime = 1.5f;
            }

            Instantiate(zombieToSpawn, transform.position, Quaternion.identity);
            Debug.Log($"{name} spawned {zombieToSpawn.name}");

            _spawnedCount++;
            FindObjectOfType<WaveController>()?.RegisterZombie();
            SetTimeUntilSpawn();
        }
    }

    public void StartSpawningManually()
    {
        spawningActive = true;
        _spawnedCount = 0;
        SetTimeUntilSpawn();
        Debug.Log($"{name} manually triggered to spawn on round {currentRound}");
    }


    public void SetRound(int round)
    {
        currentRound = round;
        _spawnedCount = 0;
        Debug.Log($"{name} updated to round {round}");
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
