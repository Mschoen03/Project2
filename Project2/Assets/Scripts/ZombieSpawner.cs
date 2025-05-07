using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float spawnStartDelay = 30f;
    private bool spawningActive = false;

    [SerializeField] private float _minimumSpawnTime = 5f;
    [SerializeField] private float _maximumSpawnTime = 8f;
    [SerializeField] private float minimumAllowedSpawnTime = 0.25f; // never drop below this
    [SerializeField] private float spawnSpeedRampRate = 0.1f; // rate of spawn time reduction

    [SerializeField] private int _maxSpawnCount = 50;

    private float _timeUntilSpawn;
    private int _spawnedCount = 0;

    private int currentRound = 1;
    private ZombieGameManager gameManager;

    void Awake()
    {
        Invoke(nameof(EnableSpawning), spawnStartDelay);

        SetTimeUntilSpawn();
        gameManager = FindObjectOfType<ZombieGameManager>();
    }

    void EnableSpawning()
    {
        spawningActive = true;
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
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            _spawnedCount++;

            if (gameManager != null)
            {
                gameManager.RegisterZombie();
            }

            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

   
    public void SetRound(int round)
    {
        currentRound = round;
        Debug.Log("Round set to: " + currentRound);
        // You could adjust difficulty here if needed
    }

    
    public void StartSpawningManually()
    {
        spawningActive = true;
        Debug.Log("Manual zombie spawning started.");
    }
}