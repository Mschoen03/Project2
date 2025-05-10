using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieGameManager : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    private bool hasWon = false;

    void Start()
    {
        if (winText != null) winText.SetActive(false);
    }

    void Update()
    {
        if (hasWon && Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void TriggerWin()
    {
        if (hasWon) return;
        hasWon = true;

        Debug.Log("You Win! All waves complete.");
        if (winText != null) winText.SetActive(true);
    }
}
