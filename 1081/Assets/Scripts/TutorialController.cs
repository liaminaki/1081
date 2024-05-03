using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> messages = new List<GameObject>();
    private int messageID = -1;
    public AudioSource messageSound;

    // Start is called before the first frame update
    void Start()
    {   
        // int hasWatchedTutorial = PlayerPrefs.GetInt("HasWatchedTutorial", 0);
        int hasWatchedTutorial = 0;
        
        if (hasWatchedTutorial == 0) 
        {
             // Set inactive all tutorial messages
            foreach (GameObject message in messages)
            {
                message.SetActive(false);
            }
        }

        else
        {
            // Turn off this TutorialController is tutorial already watched
            gameObject.SetActive(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (messageID > -1 && Input.GetMouseButtonDown(0))
        {
            // Call ShowMessage() when clicked
            ShowMessage();
        }
    }

    // public void StartMessage() 
    // {   
    //     Time.timeScale = 0f;
    //     ShowMessage();
    // }

    public void ShowMessage()
    {
        // Increment messageID first
        messageID++;

        if (messageID == 1)
            Time.timeScale = 0f;

        else if (messageID == 6)
            Time.timeScale = 1f;

        // Check if there are more messages to show
        if (messageID < messages.Count)
        {   

            for (int i = 0; i < messages.Count; i++)
            {
                if (i == messageID) {
                    // StartCoroutine(ShowMessageDelayed());
                    messages[i].SetActive(true);

                    // Play sound before setting the message active
                    if (messageSound != null && messageSound.clip != null)
                    {
                        messageSound.Play();
                    }
                }
                    
                else
                    messages[i].SetActive(false);
            }
        }
        else
        {   
            // Set to true if all tutorial messages are watched
            PlayerPrefs.SetInt("HasWatchedTutorial", 1);

            // No more messages, turn off this TutorialController
            gameObject.SetActive(false);
        }
    }

    // IEnumerator ShowMessageDelayed()
    // {
    //     yield return new WaitForSeconds(2f); // Wait for 2 seconds

    //     // Show the message corresponding to the current messageID
    //     messages[messageID].SetActive(true);
    // }
}
