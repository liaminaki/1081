using UnityEngine;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    public List<GameObject> journalPages = new List<GameObject>();

    void Start()
    {
        // Activate the first journal page by default
        ActivateJournalPage(0);
    }

    public void ActivateJournalPage(int index)
    {
        for (int i = 0; i < journalPages.Count; i++)
        {
            if (i == index)
            {
                journalPages[i].SetActive(true);
            }
            else
            {
                journalPages[i].SetActive(false);
            }
        }
    }
}