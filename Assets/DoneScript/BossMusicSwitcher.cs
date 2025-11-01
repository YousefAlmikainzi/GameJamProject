using UnityEngine;

public class BossMusicSwitcher : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] GameObject soundSystem; // your normal BGM GameObject
    [SerializeField] GameObject bossMusic;   // GameObject with the AudioSource for boss music

    void OnEnable()
    {
        if (bossMusic) bossMusic.SetActive(true);
        if (soundSystem) soundSystem.SetActive(false);
    }

    void OnDisable()
    {
        if (soundSystem) soundSystem.SetActive(true);
        if (bossMusic) bossMusic.SetActive(false);
    }

    void OnDestroy()
    {
        if (soundSystem) soundSystem.SetActive(true);
        if (bossMusic) bossMusic.SetActive(false);
    }
}
