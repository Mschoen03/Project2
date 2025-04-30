using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loss : MonoBehaviour
{
    public PlayerHealth playerHealth; // Drag your player here

    private TextMeshProUGUI LossText;  // We'll find it in the Canvas under Player

    private bool gameEnded = false;

    void Start()
    {
        // Find the Canvas object under Player and then find LossText inside it
        LossText = playerHealth.transform.Find("Canvas/LossText").GetComponent<TextMeshProUGUI>();

        // Hide the loss text initially
        LossText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded && playerHealth.GetCurrentHealth() <= 0)
        {
            TriggerLoss();
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void TriggerLoss()
    {
        gameEnded = true;
        Debug.Log("Player died — showing loss screen");
        LossText.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }
}