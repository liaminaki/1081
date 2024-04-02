using UnityEngine;
using System.Collections.Generic;

public class ShieldManager : MonoBehaviour
{
    List<int> collectedshieldIDs = new List<int>();

    void Start(){
        // ResetCollectedshields();
        loadshieldList();
        DisableCollectedshields();
    }
    

    public void loadshieldList(){
        if (PlayerPrefs.HasKey("CollectedshieldIDs"))
        {
            string shieldIDsString = PlayerPrefs.GetString("CollectedshieldIDs");
            string[] shieldIDsArray = shieldIDsString.Split(',');
            foreach (string id in shieldIDsArray)
            {
                collectedshieldIDs.Add(int.Parse(id));
            }
        }   
    }
    public void addshieldToList(int shieldID){
        collectedshieldIDs.Add(shieldID); // Add the newly collected shield ID
        string collectedshieldIDsString = string.Join(",", collectedshieldIDs); // Convert the List to a string
        PlayerPrefs.SetString("CollectedshieldIDs", collectedshieldIDsString); // Store the string in PlayerPrefs
        PlayerPrefs.Save();
        // loadshieldList();
    }

    public void ShowCollectedshieldIDs()
    {
        Debug.Log("Collected shield IDs:");
        foreach (int id in collectedshieldIDs)
        {
            Debug.Log(id);
        }
    }

    public void ResetCollectedshields()
    {
        collectedshieldIDs.Clear(); // Clear the list of collected shield IDs
        PlayerPrefs.DeleteKey("CollectedshieldIDs"); // Delete the saved data from PlayerPrefs
    }

    private void DisableCollectedshields()
    {
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
        foreach (GameObject shield in shields)
        {
            Shield shieldScript = shield.GetComponent<Shield>();
            if (shieldScript != null && collectedshieldIDs.Contains(shieldScript.ShieldID))
            {
                shield.SetActive(false); // Disable the shield GameObject
            }
        }
    }
}
