using UnityEngine;
using System.Collections.Generic;

public class CoinCollectionManager : MonoBehaviour
{
    List<int> collectedCoinIDs = new List<int>();

    void Start(){
        // ResetCollectedCoins();
        loadCoinList();
        DisableCollectedCoins();
    }
    public void loadCoinList(){
        if (PlayerPrefs.HasKey("CollectedCoinIDs"))
        {
            // If PlayerPrefs already contains a string of collected coin IDs, retrieve it and convert it back to a List
            string coinIDsString = PlayerPrefs.GetString("CollectedCoinIDs");
            string[] coinIDsArray = coinIDsString.Split(',');
            foreach (string id in coinIDsArray)
            {
                collectedCoinIDs.Add(int.Parse(id));
            }
        }   
    }
    public void addCoinToList(int coinID){
        collectedCoinIDs.Add(coinID); // Add the newly collected coin ID
        string collectedCoinIDsString = string.Join(",", collectedCoinIDs); // Convert the List to a string
        PlayerPrefs.SetString("CollectedCoinIDs", collectedCoinIDsString); // Store the string in PlayerPrefs
        PlayerPrefs.Save();
        // loadCoinList();
    }

    public void ShowCollectedCoinIDs()
    {
        Debug.Log("Collected Coin IDs:");
        foreach (int id in collectedCoinIDs)
        {
            Debug.Log(id);
        }
    }

    public void ResetCollectedCoins()
    {
        collectedCoinIDs.Clear(); // Clear the list of collected coin IDs
        PlayerPrefs.DeleteKey("CollectedCoinIDs"); // Delete the saved data from PlayerPrefs
    }

    private void DisableCollectedCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Coin coinScript = coin.GetComponent<Coin>();
            if (coinScript != null && collectedCoinIDs.Contains(coinScript.coinID))
            {
                coin.SetActive(false); // Disable the coin GameObject
            }
        }
    }
}
