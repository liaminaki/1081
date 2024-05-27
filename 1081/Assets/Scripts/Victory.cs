using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public int ChapterNum;
    private Chapter chapter;

    // Start is called before the first frame update
    public Timer timerSystem;
    public GameObject timerUI;
    public GameObject victoryScreenUI;
    public GameObject pauseUI;
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
        // Set game scene as previous scene
        SceneStateManager.PreviousScene = "GameScene";
        chapter = new Chapter(ChapterNum);
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

            pauseUI.SetActive(false);
            
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
            // PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", starsProgress.possibleStars);
            chapter.UpdateScore(starsProgress.possibleStars);

            
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

    public void GoToEpilogueIfNotWatched() 
    {   
        if (ChapterNum == 5)
        {
            int hasWatchedEpilogue = PlayerPrefs.GetInt("HasWatchedEpilogue", 0);

            if (hasWatchedEpilogue == 0)
                SceneManager.LoadScene("Epilogue");
            
            else
                SceneManager.LoadScene("ChapterSelectionScene");

        }

    }

}
