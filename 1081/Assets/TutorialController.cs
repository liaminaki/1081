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
        // Set inactive all tutorial messages
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
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

    public void ShowMessage()
    {
        // Increment messageID first
        messageID++;

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
