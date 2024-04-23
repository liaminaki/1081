using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Victory : MonoBehaviour
{
    public int ChapterNum;
    // Start is called before the first frame update
    public Timer timerSystem;
    public GameObject timerUI;
    public GameObject victoryScreenUI;
    private PlayerMovement player;
    public TMP_Text elapsedTimeText;
    public TMP_Text collectedCoinsText;
    public TMP_Text currentTotalCoins;
    public AudioSource src;
    // public AudioClip missionSuccess, stars;
    // public EndTrigger end;
    public static bool GameIsPaused = false;

    public StarsProgressController starsProgress;
    public GameObject OneStar, TwoStars, ThreeStars;
    bool lockSwitch = true;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        victoryScreenUI.SetActive(false);
    }

    public void StopTime() {
        timerSystem.StopTime();
        GameIsPaused = true;
        timerUI.SetActive(false);
    }
    
    public void Open()
    {
        if (lockSwitch)
        {
            victoryScreenUI.SetActive(true);
            
            elapsedTimeText.text = timerSystem.GetTime();
            currentTotalCoins.text = PlayerPrefs.GetInt("PlayerCoins").ToString();

            if (starsProgress.possibleStars == 3)
            {
                ThreeStars.SetActive(true);
            }
            else if (starsProgress.possibleStars == 2)
            {
                TwoStars.SetActive(true);
            }
            else if (starsProgress.possibleStars == 1)
            {
                OneStar.SetActive(true);
            }
            
            // Save stars number
            PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", starsProgress.possibleStars);

            
            collectedCoinsText.text = "+" + player.currentCoins.ToString();
            AddCoinsToPlayerPrefs(player.currentCoins);
            lockSwitch = false;
        }
    }

    void AddCoinsToPlayerPrefs(int currentCoins)
    {
        int coinCount = PlayerPrefs.GetInt("PlayerCoins", 0);
        coinCount += currentCoins;
        PlayerPrefs.SetInt("PlayerCoins", coinCount);
        PlayerPrefs.Save();
    }

}
