using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Failed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject failedScreenUI;
    public GameObject pauseUI;
    public GameObject statsUI;

    public bool loss {get; set;}
    void Start()
    {
        failedScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.L)){
        //     loss = true;
        // }
        if (loss)
        {
            failedScreenUI.SetActive(true);
            pauseUI.SetActive(false);
            Time.timeScale = 0f;
            statsUI.SetActive(false);
        }
    }
    public void Exit()
    {
        loss = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChapterSelectionScene");

    }

    public void Retry()
    {
        Time.timeScale = 1f;
        loss = false; 
        failedScreenUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
