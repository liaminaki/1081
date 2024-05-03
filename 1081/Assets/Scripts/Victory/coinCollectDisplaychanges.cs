using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class coinCollectDisplaychanges : MonoBehaviour
{
    // Start is called before the first frame update
    int currentTotalCoins;
    PlayerMovement player ;
    public TMP_Text currentTotalCoinsText;

    public AudioSource aud;

    public void Start(){
        player = FindObjectOfType<PlayerMovement>();
    }
    public void PlaySound(){
        aud.Play();
    }
    void DisplayChangesinCoins()
    {
        int currentTotalCoins = int.Parse(currentTotalCoinsText.text);
        float startTime = Time.realtimeSinceStartup; // Use system clock time
        float delayTime = 100f; // Adjust delay time in seconds

        for (int i = currentTotalCoins; i <= currentTotalCoins + player.currentCoins; i++)
        {
            if (Time.realtimeSinceStartup - startTime >= delayTime)
            {
                currentTotalCoinsText.text = i.ToString();
                startTime = Time.realtimeSinceStartup;
            }
        }
        currentTotalCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
    }
}
