using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieGameManager : MonoBehaviour
{
    [SerializeField] private GameObject winText;

    private int zombiesSpawned = 0;
    private int zombiesKilled = 0;

    private bool hasWon = false;

    void Start()
    {
        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    void Update()
    {
        if (hasWon)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene("Credits"); // Replace with exact name of your credits scene
            }
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

        hasWon = true;
    }
}