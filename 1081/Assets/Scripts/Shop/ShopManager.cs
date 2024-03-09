using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceTag;
    private string price;
    private bool shieldUpgrade = false;
    private bool staminaUpgrade = false;
    private bool shieldPurchase = false;

    public void Start()
    {
        GetPrice();
    }

    public void Update()
    {
        CheckPurchase();
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
        }
        else
        {
            Debug.LogError("GameObject reference is null.");
        }
    }

    // Method to handle button clicks
    public void OnButtonClicked()
    {
        if (price != null)
        {
            // Attempt to parse the text to an integer
            int amount;
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

            // Deduct the amount from player coins
            DeductCoins(amount);
        }
        else
        {
            Debug.Log("Price is null");
        }
    }

    // Method to simulate deducting coins
    private void DeductCoins(int amount)
    {
        // Return true if deduction is successful, false otherwise
        //uncomment this if testing 
        //PlayerPrefs.SetInt("PlayerCoins", 1000);
        //PlayerPrefs.Save();
        int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
        if (playerCoins >= amount)
        {
            playerCoins -= amount;
            PlayerPrefs.SetInt("PlayerCoins", playerCoins);
            PlayerPrefs.Save();
            // Success: Perform upgrade or other action
            Debug.Log("Successfully deducted " + amount + " coins.");
        }
        else
        {
            Debug.Log("Insufficient coins!");
        }
    }

    public void CheckPurchase()
    {
        //uncomment this if testing
        //PlayerPrefs.SetInt("ShieldLevel", 1);
        //PlayerPrefs.SetInt("StaminaLevel", 1);
        //PlayerPrefs.SetInt("ShieldNumber", 0);
        //PlayerPrefs.Save();
        if (shieldUpgrade)
        {
            int shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
            PlayerPrefs.SetInt("ShieldLevel", shieldLevel + 1);
            PlayerPrefs.Save();
            shieldUpgrade = false;
        }
        else if (staminaUpgrade)
        {
            int staminaLevel = PlayerPrefs.GetInt("StaminaLevel");
            PlayerPrefs.SetInt("StaminaLevel", staminaLevel + 1);
            PlayerPrefs.Save();
            staminaUpgrade = false;
        }
        else if (shieldPurchase)
        {
            int shieldNumber = PlayerPrefs.GetInt("ShieldNumber");
            PlayerPrefs.SetInt("ShieldNumber", shieldNumber + 1);
            PlayerPrefs.Save();
            shieldPurchase = false;
        }
    }

    public void ShieldUpgrade(bool value)
    {
        this.shieldUpgrade = value;
    }

    public void StaminaUpgrade(bool value)
    {
        this.staminaUpgrade = value;
    }

    public void ShieldPurchase(bool value)
    {
        this.shieldPurchase = value;
    }
}
