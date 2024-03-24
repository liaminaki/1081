using System.Collections.Generic;
using UnityEngine;

/*
    Contains data about a single dialogue (with list of dialogue lines)
    Send message to DialogueManager class
*/
 
[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}
 
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    
    // Set the number of lines and the maximum number of characters
    [TextArea(3, 60)]
    public string LineInEnglish;
    [TextArea(3, 60)]
    public string LineInCebuano;
    [TextArea(3, 60)]
    public string LineInFilipino;
}
 
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
 
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
 
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TriggerDialogue();
        }
    }
}