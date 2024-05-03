using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndTrigger : MonoBehaviour
{
    // References to the timelines
    public PlayableDirector MainTimeline;
    
    // The renamed reference to the post-game timeline is now called Timeline2
    public PlayableDirector Timeline2;
    // public bool end = false;

    // A flag to track whether the event has been handled
    private bool eventHandled = false;

    public Victory victory;
    public bool Win {get; private set;}

    // This method is called at the start of the script's life
    void Start()
    {
        // Play the MainTimeline at the beginning of the game
        if (MainTimeline != null)
        {
            MainTimeline.Play();
        }
        
        // Ensure Timeline2 is stopped at the start of the game
        if (Timeline2 != null)
        {
            Timeline2.Stop();
        }
        Win = false;
    }

    // This method is called when another collider enters the collider attached to this game object
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the event has already been handled
        if (eventHandled)
        {
            // Exit the method if the event has been handled to avoid duplicate execution
            return;
        }

        // Check if the collided object is tagged as "Player"
        if (collider.CompareTag("Player"))
        {
            // end = true;
            // Log the collision for debugging purposes
            Debug.Log("Player collided with the end trigger.");
            Win = true;

            // Stop the MainTimeline if it's currently playing
            if (MainTimeline != null && MainTimeline.state == PlayState.Playing)
            {
                MainTimeline.Stop();
                Debug.Log("MainTimeline stopped.");
            }

            // Play Timeline2
            if (Timeline2 != null)
            {
                Timeline2.Play();
                Debug.Log("Timeline2 started.");
            }
            
            victory.StopTime();
            // victory.Open();

            // Set the eventHandled flag to true to prevent future execution
            eventHandled = true;
        }
    }
}


