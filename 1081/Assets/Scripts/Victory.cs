using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Victory : MonoBehaviour
{
    // Start is called before the first frame update
    public Timer timerSystem;
    public GameObject timerUI;
    public GameObject victoryScreenUI;
    public PlayerMovement player;
    public TMP_Text elapsedTimeText;
    public TMP_Text collectedCoinsText;
    public AudioSource src;
    public AudioClip missionSuccess, stars;
    public static bool GameIsPaused = false;

    public StarsProgressController starsProgress;
    public GameObject OneStar, TwoStars, ThreeStars;
    void Start()
    {
        victoryScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("8 has been pressed!");
            // victoryScreenUI.SetActive(true);
            // Time.timeScale = 0f;

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
    public void Resume()
    {
        victoryScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        victoryScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        elapsedTimeText.text = timerSystem.GetTime();
        
        if(starsProgress.possibleStars == 3){
            ThreeStars.SetActive(true);
        }
        else if(starsProgress.possibleStars == 2){
            TwoStars.SetActive(true);
        }
        else if(starsProgress.possibleStars == 1){
            OneStar.SetActive(true);
        }

        timerUI.SetActive(false);
        collectedCoinsText.text = "+" + player.currentCoins.ToString();
        AddCoinsToPlayerPrefs(player.currentCoins);
    }
    void AddCoinsToPlayerPrefs(int currentCoins)
    {
        int coinCount = PlayerPrefs.GetInt("PlayerCoins", 0);
        coinCount += currentCoins;
        PlayerPrefs.SetInt("PlayerCoins", coinCount);
        PlayerPrefs.Save();
    }
}
