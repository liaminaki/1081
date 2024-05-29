using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int chapterNum;
    private bool unlocked = false;
    public GameObject toolTip;

    void Start()
    {   
        toolTip.SetActive(false);
        UnityEngine.UI.Button buttonComponent = GetComponent<UnityEngine.UI.Button>();

        int currentChapterScore = PlayerPrefs.GetInt("Chapter" + chapterNum + "Stars", 0);
        
        // Unlock button if chapter finished playing 
        // Chapter is finished if score is at least 1
        if (currentChapterScore > 0)
        {
            unlocked = true;
            buttonComponent.interactable = true;
        } 
        
        else 
        {   
            unlocked = false;
            buttonComponent.interactable = false;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   
        // Show tooltip
        if (!unlocked)
            toolTip.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide tooltip
        if (!unlocked)
            toolTip.SetActive(false);
    }



}