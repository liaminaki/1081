using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const string playerCoinsKey = "PlayerCoins";
    private int playerCoins;
    private const string playerShieldLevelKey = "ShieldLevel";
    private int playerShieldLevel;
    private const string playerStaminaLevelKey = "StaminaLevel";
    private int playerStaminaLevel;
    private const string playerShieldNumberKey = "ShieldNumber";
    private int playerShieldNumber;

    // Start is called before the first frame update
    private void Start()
    {
        //Load player coins from PlayerPrefs, defaulting to 200 if not found
        playerCoins = PlayerPrefs.GetInt(playerCoinsKey, 20);
        playerShieldLevel = PlayerPrefs.GetInt(playerShieldLevelKey, 1);
        playerStaminaLevel = PlayerPrefs.GetInt(playerStaminaLevelKey, 1);
        playerShieldNumber = PlayerPrefs.GetInt(playerShieldNumberKey, 3);
        SavePlayerCoins();
        SaveShieldLevel();
        SaveStaminaLevel();
        SaveShieldNumber();
    }
    
    //Function to save player coins to PlayerPrefs
    private void SavePlayerCoins()
    {
        //sets the new player coins
        PlayerPrefs.SetInt(playerCoinsKey, playerCoins);
        PlayerPrefs.Save();
    }
    private void SaveShieldLevel()
    {
        //sets the new player coins
        PlayerPrefs.SetInt(playerShieldLevelKey, playerShieldLevel);
        PlayerPrefs.Save();
    }
    private void SaveStaminaLevel()
    {
        //sets the new player coins
        PlayerPrefs.SetInt(playerStaminaLevelKey, playerStaminaLevel);
        PlayerPrefs.Save();
    }
    private void SaveShieldNumber()
    {
        //sets the new player coins
        PlayerPrefs.SetInt(playerShieldNumberKey, playerShieldNumber);
        PlayerPrefs.Save();
    }
}
