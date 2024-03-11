using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceTag;
    private string price;
    private int amount;

    public void Start()
    {
        GetPrice();
    }

    public void GetPrice()
    {
        if (_priceTag != null)
        {
            TextMeshProUGUI priceTag = _priceTag.GetComponent<TextMeshProUGUI>();

            if (priceTag != null)
            {
                price = priceTag.text;
            }
            else
            {
                Debug.LogError("Text component not found on the GameObject.");
            }

            //uncomment this if funds are not enough during testing 
            //PlayerPrefs.SetInt("PlayerCoins", 1000);
            //PlayerPrefs.SetInt("ShieldLevel", 1);
            //PlayerPrefs.SetInt("StaminaLevel", 1);
            //PlayerPrefs.SetInt("ShieldNumber", 0);
            //PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("GameObject reference is null.");
        }
    }

    public void ShieldUpgrade()
    {
        if (price != null)
        {
            // Attempt to parse the text to an integer
            try
            {
                amount = int.Parse(price);
            }
            catch (FormatException)
            {
                Debug.LogError("Button text is not a valid integer.");
                return;
            }
            catch (OverflowException)
            {
                Debug.LogError("Button text is too large to be represented as an integer.");
                return;
            }

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            int shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
            if (playerCoins >= amount)
            {
                if (shieldLevel < 5)
                {
                    playerCoins -= amount;
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                    PlayerPrefs.SetInt("ShieldLevel", shieldLevel + 1);
                    PlayerPrefs.Save();
                    // Success: Perform upgrade or other action
                    Debug.Log("Successfully deducted " + amount + " coins.");
                }
                else
                {
                    Debug.Log("Maximum Shield Level Reached!");
                }
            }
            else
            {
                Debug.Log("Insufficient Funds!");
            }
        }
        else
        {
            Debug.Log("Price is null");
        }
    }

    public void StaminaUpgrade()
    {
        if (price != null)
        {
            // Attempt to parse the text to an integer
            try
            {
                amount = int.Parse(price);
            }
            catch (FormatException)
            {
                Debug.LogError("Button text is not a valid integer.");
                return;
            }
            catch (OverflowException)
            {
                Debug.LogError("Button text is too large to be represented as an integer.");
                return;
            }

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            int staminaLevel = PlayerPrefs.GetInt("StaminaLevel");
            if (playerCoins >= amount)
            {
                if (staminaLevel < 5)
                {
                    playerCoins -= amount;
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                    PlayerPrefs.SetInt("StaminaLevel", staminaLevel + 1);
                    PlayerPrefs.Save();
                    // Success: Perform upgrade or other action
                    Debug.Log("Successfully deducted " + amount + " coins.");
                }
                else
                {
                    Debug.Log("Maximum Stamina Level Reached!");
                }
            }
            else
            {
                Debug.Log("Insufficient Funds!");
            }
        }
        else
        {
            Debug.Log("Price is null");
        }
    }

    public void ShieldPurchase()
    {
        if (price != null)
        {
            // Attempt to parse the text to an integer
            try
            {
                amount = int.Parse(price);
            }
            catch (FormatException)
            {
                Debug.LogError("Button text is not a valid integer.");
                return;
            }
            catch (OverflowException)
            {
                Debug.LogError("Button text is too large to be represented as an integer.");
                return;
            }

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            int shieldNumber = PlayerPrefs.GetInt("ShieldNumber");
            if (playerCoins >= amount)
            {
                playerCoins -= amount;
                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                PlayerPrefs.SetInt("ShieldNumber", shieldNumber + 1);
                PlayerPrefs.Save();
                // Success: Perform upgrade or other action
                Debug.Log("Successfully deducted " + amount + " coins.");
            }
            else
            {
                Debug.Log("Insufficient Funds!");
            }
        }
        else
        {
            Debug.Log("Price is null");
        }
    }
}
