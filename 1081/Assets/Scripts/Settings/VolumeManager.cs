using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer _audio;
    public Slider volumeSlider;


private void Start()
    {
        // Add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(setVolume);
        Debug.Log("Volume Controller initialized.");

        // Load the volume value from PlayerPrefs and set it to the slider
        float savedVolume = PlayerPrefs.GetFloat("Volume", -7.0f); // Default value 1.0f if not found
        volumeSlider.value = savedVolume;

        // Apply the initial volume value
        setVolume(savedVolume);
    }

    public void setVolume(float vol){
        
        if (Mathf.Approximately(vol, volumeSlider.minValue)) // If the slider is at the left end
        {
            vol = -80f; // Set volume to 0
        }

        _audio.SetFloat("vol", vol);
        PlayerPrefs.SetFloat("Volume", vol);
    }

}
