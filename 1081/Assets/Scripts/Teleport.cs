using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public GameObject objectToTeleport; // GameObject to teleport
    public float teleportPositionX; // New x position to teleport to
    public float teleportPositionY; // New y position to teleport to
    public PlayableDirector timeline; // Timeline to play on trigger

    private void Awake()
    {
        // Turn off the timeline at the start
        if (timeline != null)
        {
            timeline.gameObject.SetActive(false);
        }
    }

    // Called when the GameObject collides with another Collider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as "TeleportZone"
        if (other.CompareTag("Player"))
        {   
            // MoveObject();
            StartCoroutine(PlayTimelineThenMoveObject());
        }
    }

    // Plays the timeline
    private void PlayTimeline()
    {
        if (timeline != null)
        {   
            timeline.gameObject.SetActive(true);
            timeline.Play();
        }
        else
        {
            Debug.LogWarning("No timeline assigned!");
        }
    }

    // Moves the object to the specified position
    private void MoveObject()
    {
        if (objectToTeleport != null)
        {
            objectToTeleport.transform.position = new Vector3(teleportPositionX, teleportPositionY, objectToTeleport.transform.position.z);
        }
        else
        {
            Debug.LogWarning("No GameObject assigned to teleport!");
        }
    }

    // Coroutine to play the timeline first and then move the object after a delay
    IEnumerator PlayTimelineThenMoveObject()
    {
        PlayTimeline(); // Play the timeline first

        // Wait for one second
        yield return new WaitForSeconds(1f);

        MoveObject(); // Move the object to the specified position
    }
}
