using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public float TypingSpeed = 0.2f; // Typing speed in seconds

    // List to hold TextMeshPro components
    public List<TextMeshProUGUI> TextObjects = new List<TextMeshProUGUI>();

    // Audio clip to play for each character
    public AudioClip TypingSound;

    // AudioSource component to play the typing sound
    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {   
        // Turn off all text objects initially
        TurnOffTexts();

        // Start typing the first text in the list
        StartTypingNextText();
    }

    // Start typing the next text, if available
    private void StartTypingNextText()
    {
        if (TextObjects.Count > 0)
        {
            TextMeshProUGUI text = TextObjects[0];
            StartCoroutine(TypeText(text));
        }
    }

    private void TurnOffTexts()
    {
        foreach (TextMeshProUGUI textObject in TextObjects)
        {
            textObject.gameObject.SetActive(false);
        }

    }

    // Coroutine to type the TextMeshPro component
    private IEnumerator TypeText(TextMeshProUGUI text)
    {
        string originalText = text.text;
        text.text = "";
        text.gameObject.SetActive(true); // Activate the text object

        // Display text by character like typing animation
        foreach (char letter in originalText.ToCharArray())
        {
            text.text += letter;

            // Play typing sound
            if (TypingSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(TypingSound);
            }
            
            yield return new WaitForSeconds(TypingSpeed);
        }

        // Remove the typed TextMeshPro component from the list
        TextObjects.RemoveAt(0);

        // Start typing the next text, if available
        StartTypingNextText();
    }
}