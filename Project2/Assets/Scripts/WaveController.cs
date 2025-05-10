using System.Collections;
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

    private RoundUI roundUI; // UI reference (optional)

    void Start()
    {
        roundUI = FindObjectOfType<RoundUI>(); // optional round display
    }

    // Called externally (e.g., from DialogManager)
    public void StartWaves()
{
    if (!wavesStarted)
    {
        Debug.Log("[WaveController] StartWaves() called.");
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
        if (activeZombies <= 0)
        {
            if (currentRound < maxRounds)
                StartCoroutine(NextWaveDelay());
            else
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
        roundUI?.UpdateRound(currentRound);

        foreach (ZombieSpawner spawner in zombieSpawners)
        {
            if (spawner != null)
            {
                Debug.Log($"[WaveController] Triggering spawner: {spawner.name} at round {currentRound}");
                spawner.SetRound(currentRound);
                spawner.StartSpawningManually(); // ✅ Always start spawning
            }
        }

        yield return null;
    }

}
