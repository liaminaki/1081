using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer _audio;
    public GameObject ON;
    public GameObject OFF;


    public void setVolume(float vol){
        _audio.SetFloat("vol", vol);
        PlayerPrefs.SetFloat("VolumeLevel", vol);
        // selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex");
    }

    public void On(){
        AudioListener.volume = 0;
        ON.SetActive(false);
        OFF.SetActive(true);
    }

    public void Off(){
        AudioListener.volume = 1;
        OFF.SetActive(false);
        ON.SetActive(true);
    }

}
