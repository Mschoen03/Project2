using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public AudioClip zombieSound;  // Drag the zombie sound here
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayZombieSound();  // Or trigger later via animation or detection
    }

    public void PlayZombieSound()
    {
        if (zombieSound != null)
        {
            audioSource.PlayOneShot(zombieSound);
        }
    }
}
