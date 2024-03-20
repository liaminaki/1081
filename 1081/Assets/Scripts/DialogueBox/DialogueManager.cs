using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

/*  
    Show/Hide Dialogue Box
    Update Text and Icons
*/
 
public class DialogueManager : MonoBehaviour
{   
    // Singleton to allow access anywhere in the game
    public static DialogueManager Instance;

    // Timeline container
    public PlayableDirector playableDirector;
 
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
 
    private Queue<DialogueLine> lines;
    
    public bool isDialogueActive = false; 
 
    public float typingSpeed = 0.2f;
    
    // For opening and closing animation
    public Animator animator;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
 
        lines = new Queue<DialogueLine>();
    }

    private void Start() {
        // Ensure that the PlayableDirector reference is assigned
        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirector reference is not assigned!");
            return;
        }
    }
 
    public void StartDialogue(Dialogue dialogue)
    {
        PauseTimeline();

        isDialogueActive = true;
 
        animator.Play("show");
 
        lines.Clear();

        // Add dialogue lines in queue
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        // Show first dialogue
        DisplayNextDialogueLine();
    }
 
    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        DialogueLine currentLine = lines.Dequeue();
 
        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;
 
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(currentLine));
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";

        // Display text by character like typing animation
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
 
    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");

        ResumeTimeline();
    }

    // Function to pause the Timeline
    void PauseTimeline()
    {
        // Pause the Timeline by setting its speed to 0
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    // Function to resume the Timeline
    void ResumeTimeline()
    {
        // Resume the Timeline by setting its speed back to 1
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        
    }
}