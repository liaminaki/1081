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
    [Header("Dialogue System")]
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
    private string language = "";

    [Header("Audio")]
    public AudioClip typingSound; // Sound to play while typing

    public AudioSource audioSource;

    [Header("Dialogue Skin")]
    public Sprite Male;
    public Sprite Female;
 
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

        LoadSelectedLanguage();
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
        // Stop typing sound after the animation is complete
        if (typingSound != null && audioSource != null)
            audioSource.Stop();

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        DialogueLine currentLine = lines.Dequeue();


        // Set saved player details
        if (currentLine.character.name == "Player") {
            
            characterName.text = LoadPlayerName();

            int skin = LoadPlayerSkin();

            if (skin == 0)
                characterIcon.sprite = Female;
            else
                characterIcon.sprite = Male;
        }

        else {
            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;
        }
        
 
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(currentLine));
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        string line = "";
        
        switch (language)
        {
            case "ENGLISH":
                line = dialogueLine.LineInEnglish;
                break;
            case "CEBUANO":
                line = dialogueLine.LineInCebuano;
                break;
            case "FILIPINO":
                line = dialogueLine.LineInFilipino;
                break;
        }

         // Play typing sound at the beginning
        if (typingSound != null && audioSource != null) {
            audioSource.clip = typingSound;
            // audioSource.loop = true;
            audioSource.Play();
        }

        // Display text by character like typing animation
        foreach (char letter in line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

         // Stop typing sound after the animation is complete
        if (typingSound != null && audioSource != null)
            audioSource.Stop();
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

    private void LoadSelectedLanguage()
    {
        language = PlayerPrefs.GetString("Language");
    }

    private string LoadPlayerName() {
        return PlayerPrefs.GetString("CharacterName");
    }

    private int LoadPlayerSkin() {
        return PlayerPrefs.GetInt("SelectedSkinIndex");
    }
}