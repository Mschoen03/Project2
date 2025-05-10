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
            // ⏩ Skip to end of sentence
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
            // ✅ Dialogue finished
            textbox.text = "";
            ContinueButton.SetActive(false);
            dialogPanel.SetActive(false);

            // ✅ Trigger all assigned zombie spawners
            foreach (ZombieSpawner spawner in zombieSpawners)
            {
                if (spawner != null)
                {
                    spawner.StartSpawningManually();
                }
            }
        }
    }
}
