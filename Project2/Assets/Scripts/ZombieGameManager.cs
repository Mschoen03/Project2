using UnityEngine;

public class ZombieGameManager : MonoBehaviour
{
    [SerializeField] private GameObject winText;

    private int zombiesSpawned = 0;
    private int zombiesKilled = 0;

    void Start()
    {
        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    public void RegisterZombie()
    {
        zombiesSpawned++;
    }

    public void ZombieDied()
    {
        zombiesKilled++;

        if (zombiesKilled >= zombiesSpawned)
        {
            ShowWinMessage();
        }
    }

    private void ShowWinMessage()
    {
        if (winText != null)
        {
            winText.SetActive(true);
        }
    }
}