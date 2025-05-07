using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public ZombieSpawner[] zombieSpawners;
    public float delayBeforeNextWave = 20f;
    public int maxRounds = 5;
    public ZombieGameManager gameManager;

    private int activeZombies = 0;
    private int currentRound = 0;
    private bool wavesStarted = false;

    private RoundUI roundUI; // ✅ Reference to round display

    void Start()
    {
        roundUI = FindObjectOfType<RoundUI>(); // ✅ Locate RoundUI once
    }

    public void StartWaves()
    {
        if (!wavesStarted)
        {
            wavesStarted = true;
            currentRound = 1;
            StartCoroutine(StartWave());
        }
    }

    public void RegisterZombie()
    {
        activeZombies++;
    }

    public void DeregisterZombie()
    {
        activeZombies--;
        if (activeZombies <= 0 && currentRound < maxRounds)
        {
            StartCoroutine(NextWaveDelay());
        }
        else if (activeZombies <= 0 && currentRound == maxRounds)
        {
            gameManager?.TriggerWin();
        }
    }

    private IEnumerator NextWaveDelay()
    {
        currentRound++;

        if (currentRound > maxRounds)
        {
            gameManager?.TriggerWin();
            yield break;
        }

        Debug.Log($"All zombies dead. Wave {currentRound} starts in {delayBeforeNextWave} seconds...");
        yield return new WaitForSeconds(delayBeforeNextWave);
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        Debug.Log($"[WaveController] Wave {currentRound} started!");

        // ✅ Update round display on screen
        roundUI?.UpdateRound(currentRound);

        foreach (ZombieSpawner spawner in zombieSpawners)
        {
            if (spawner != null)
            {
                Debug.Log($"[WaveController] Triggering spawner: {spawner.name} at round {currentRound}");
                spawner.SetRound(currentRound);
                spawner.StartSpawningManually();  // ✅ Corrected: No argument passed
            }
        }

        yield return null;
    }
}