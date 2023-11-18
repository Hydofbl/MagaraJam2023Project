using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        AudioManager.instance.ChangeMasterVolume(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(val => AudioManager.instance.ChangeMasterVolume(val));
    }
}
