using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource, effectsSource;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ChangeMasterVolume(0.3f);
    }
    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleSound()
    {
        audioSource.mute = !audioSource.mute;
    }

    public void ToggleEffects()
    {
        effectsSource.mute = !effectsSource.mute;
    }
}