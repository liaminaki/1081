using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Failed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject failedScreenUI;
    public GameObject statsUI;

    public bool loss;
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
            Time.timeScale = 0f;
            statsUI.SetActive(false);
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene("ChapterSelectionScene");
        Time.timeScale = 1f;

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        failedScreenUI.SetActive(false);
        Time.timeScale = 1f;
        loss = false;
    }
}
