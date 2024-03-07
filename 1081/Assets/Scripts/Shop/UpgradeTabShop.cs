using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabShop : MonoBehaviour
{
    [SerializeField] private Button _purchasedButton1;
    [SerializeField] private Button _purchasedButton2;
    [SerializeField] private Button _purchasedButton3;

    // Start is called before the first frame update
    void Start()
    {
        // Add click listeners to the buttons
        //_purchasedButton1.onClick.AddListener(() => OnButtonClicked(_purchasedButton1));
        //_purchasedButton2.onClick.AddListener(() => OnButtonClicked(_purchasedButton2));
        //_purchasedButton3.onClick.AddListener(() => OnButtonClicked(_purchasedButton3));
    }

    // Method to handle button clicks
    private void OnButtonClicked(Button button)
    {
        // Get the text component from the button's children
        Text buttonText = button.GetComponentInChildren<Text>();

        // If the text component is null, return
        if (buttonText == null)
        {
            Debug.LogError("Button does not have a Text component.");
            return;
        }

        // Attempt to parse the text to an integer
        int amount;
        try
        {
            amount = int.Parse(buttonText.text);
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

        // Deduct the amount from player coins (replace this with your actual deduction logic)
        if (DeductCoins(amount))
        {
            // Success: Perform upgrade or other action
            Debug.Log("Successfully deducted " + amount + " coins.");
        }
        else
        {
            // Insufficient coins: Display error message or handle as needed
            Debug.Log("Insufficient coins to purchase the upgrade.");
        }
    }

    // Method to simulate deducting coins (replace this with your actual deduction logic)
    private bool DeductCoins(int amount)
    {
        // Replace this with your actual logic to check if the player has enough coins and deduct them
        // Return true if deduction is successful, false otherwise
        // For example:
        // if (playerCoins >= amount)
        // {
        //     playerCoins -= amount;
        //     SavePlayerCoins();
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }
        return true; // Placeholder return value
    }

    // Update is called once per frame
    void Update()
    {

    }
}
