using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    [SerializeField]
    private int _maxSpawnCount = 50; // ? NEW: max zombies to spawn

    private float _timeUntilSpawn;
    private int _spawnedCount = 0;   // ? NEW: current zombies spawned

    private ZombieGameManager gameManager;

    void Awake()
    {
        SetTimeUntilSpawn();
        gameManager = FindObjectOfType<ZombieGameManager>();
    }

    void Update()
    {
        if (_spawnedCount >= _maxSpawnCount)
            return; // ? Stop spawning once limit reached

        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            _spawnedCount++; // ? Track how many have been spawned

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
}
