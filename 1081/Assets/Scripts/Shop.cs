using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const string playerCoinsKey = "PlayerCoins";
    private int playerCoins;

    // Start is called before the first frame update
    private void Start()
    {
        //Load player coins from PlayerPrefs, defaulting to 200 if not found
        playerCoins = PlayerPrefs.GetInt(playerCoinsKey, 200);
    }
    
    //Function to save player coins to PlayerPrefs
    private void SavePlayerCoins()
    {
        PlayerPrefs.SetInt(playerCoinsKey, playerCoins);
        PlayerPrefs.Save();
    }
}
