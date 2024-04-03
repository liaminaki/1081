using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceTag;
    [SerializeField] private TMP_Text _shieldPrice;
    [SerializeField] private TMP_Text _staminaPrice;
    [SerializeField] GameObject purchaseFailed;
    [SerializeField] GameObject purchaseSuccess;
    [SerializeField] GameObject maxLevel;

    private string price;
    private int amount;

    public void Start(){
        ShieldPrice();
        StaminaPrice();
        GetPrice();
    }
    public void Update(){
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
            PlayerPrefs.SetInt("PlayerCoins", 1000);
            // PlayerPrefs.SetInt("ShieldLevel", 1);
            // PlayerPrefs.SetInt("StaminaLevel", 1);
            // PlayerPrefs.SetInt("ShieldNumber", 0);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("GameObject reference is null.");
        }
    }

    public void ShieldPrice(){
        int shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
        int newPrice = 40 + (20*(shieldLevel - 1));
        if (shieldLevel == 5){
            _shieldPrice.text = "MAX";
        }
        else{
            _shieldPrice.text = newPrice.ToString();
        }
    }

    public void StaminaPrice(){
        int staminaLevel = PlayerPrefs.GetInt("ShieldLevel");
        int newPrice = 40 + (20*(staminaLevel - 1));
        if (staminaLevel == 5){
            _staminaPrice.text = "MAX";
        }
        else{
            _staminaPrice.text = newPrice.ToString();
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
                maxLevel.SetActive(true);
                return;
            }
            catch (OverflowException)
            {
                Debug.LogError("Button text is too large to be represented as an integer.");
                return;
            }

            int shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");

            if (shieldLevel < 5)
            {
                if (playerCoins >= amount)
                {
                    playerCoins -= amount;
                    // Success: Perform upgrade or other action
                    purchaseSuccess.SetActive(true);
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                    PlayerPrefs.SetInt("ShieldLevel", shieldLevel + 1);
                    PlayerPrefs.Save();
                    shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
                    int newPrice = 40 + (20*(shieldLevel - 1));
                    if(shieldLevel == 5){
                        _priceTag.text = "MAX";
                    }
                    else{
                        _priceTag.text = newPrice.ToString();
                    }
                }
                else
                {
                    purchaseFailed.SetActive(true);
                }
            }
            else{
                    maxLevel.SetActive(true);
            }
        }
        else
        {
            maxLevel.SetActive(true);
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
                maxLevel.SetActive(true);
                return;
            }
            catch (OverflowException)
            {
                Debug.LogError("Button text is too large to be represented as an integer.");
                return;
            }

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            int staminaLevel = PlayerPrefs.GetInt("StaminaLevel");
            if (staminaLevel < 5){
                if (playerCoins >= amount){
                    playerCoins -= amount;
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                    PlayerPrefs.SetInt("StaminaLevel", staminaLevel + 1);
                    PlayerPrefs.Save();
                    staminaLevel = PlayerPrefs.GetInt("StaminaLevel");
                    int newPrice = 40 + (20*(staminaLevel - 1));
                    if(staminaLevel == 5){
                        _priceTag.text = "MAX";
                    }
                    else{
                        _priceTag.text = newPrice.ToString();
                    }
                    // Success: Perform upgrade or other action
                    purchaseSuccess.SetActive(true);
                }
                else{
                    purchaseFailed.SetActive(true);
                }
            }
            else{
                maxLevel.SetActive(true);
            }
        }
        else{
            maxLevel.SetActive(true);
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
                purchaseSuccess.SetActive(true);

            }
            else
            {
                purchaseFailed.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Price is null");
        }
    }
}
