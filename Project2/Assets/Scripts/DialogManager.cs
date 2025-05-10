using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] senetences;
    private int index;
    public float typingSpeed;

    public GameObject ContinueButton;
    public GameObject dialogPanel;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    // ✅ Assign all spawners in the Inspector
    public ZombieSpawner[] zombieSpawners;

    void OnEnable()
    {
        ContinueButton.SetActive(true);
        StartTyping();
    }

    void StartTyping()
    {
        textbox.text = "";
        typingCoroutine = StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        isTyping = true;
        textbox.text = "";

        foreach (char letter in senetences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextSentence()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            textbox.text = senetences[index];
            isTyping = false;
            return;
        }

        if (index < senetences.Length - 1)
        {
            index++;
            StartTyping();
        }
        else
        {
            textbox.text = "";
            ContinueButton.SetActive(false);
            dialogPanel.SetActive(false);

            // ✅ Use WaveController, not spawners directly
            WaveController waveController = FindObjectOfType<WaveController>();
            if (waveController != null)
            {
                waveController.StartWaves();
                Debug.Log("[DialogManager] Waves started!");
            }
            else
            {
                Debug.LogError("WaveController not found in scene.");
            }
        }
    }

}
