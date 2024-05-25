using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : MonoBehaviour
{   
    public bool isContinuous = false;
    public float TypingSpeed = 0.05f; // Typing speed in seconds

    // List to hold TextMeshPro components
    public List<TextMeshProUGUI> TextObjects = new List<TextMeshProUGUI>();

    public AudioClip typingSound; // Sound to play while typing

    public AudioSource audioSource;


    void Awake()
    {   
        if (!isContinuous)
            gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {   
        // Check if the GameObject is active
        if (gameObject.activeSelf)
        {
            // Turn off all text objects initially
            TurnOffTexts();
            
            // Start typing the first text in the list
            StartTypingNextText();
        }
    }

    // Start typing the next text, if available
    private void StartTypingNextText()
    {
        if (TextObjects.Count > 0)
        {
            TextMeshProUGUI text = TextObjects[0];
            text.gameObject.SetActive(true);
            StartCoroutine(TypeText(text));
        }
    }

    private void TurnOffTexts()
    {   
        if (TextObjects.Count > 0)
        {
            foreach (TextMeshProUGUI textObject in TextObjects)
            {
                textObject.gameObject.SetActive(false);
            }
        }
        
    }

    // Coroutine to type the TextMeshPro component
    private IEnumerator TypeText(TextMeshProUGUI text)
    {
        string originalText = text.text;
        text.text = "";
        text.gameObject.SetActive(true); // Activate the text object

        // Play typing sound at the beginning
        if (typingSound != null && audioSource != null) {
            audioSource.clip = typingSound;
            // audioSource.loop = true;
            audioSource.Play();
        }

        // Display text by character like typing animation
        foreach (char letter in originalText.ToCharArray())
        {
            text.text += letter;

            yield return new WaitForSeconds(TypingSpeed);
        }

        // Stop typing sound after the animation is complete
        if (typingSound != null && audioSource != null)
            audioSource.Stop();

        if (isContinuous) {
            // Remove the typed TextMeshPro component from the list
            TextObjects.RemoveAt(0);

            // Start typing the next text, if available
            StartTypingNextText();
        }

        else
            StartCoroutine(DelayThenStartNext(text));
        
    }

    // One second delay
    private IEnumerator DelayThenStartNext(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(1.0f);

        text.gameObject.SetActive(false);

        // Remove the typed TextMeshPro component from the list
        TextObjects.RemoveAt(0);

        // Start typing the next text, if available
        StartTypingNextText();

    }
}