using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private int _characterLimit = 20;

    private void Start()
    {
        if (_inputField != null && _warningText != null)
        {
            // Add a listener to the onValidateInput event of the input field
            _inputField.onValidateInput += ValidateInput;

            // Add a listener to the onValueChanged event of the input field
            _inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
        else
        {
            Debug.LogError("InputField or WarningText is not assigned in the inspector.");
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // Check if the input exceeds the character limit
        if (text.Length + 1 > _characterLimit)
        {
            // Display warning text
            _warningText.text = "Maximum of 20 characters only.";
            return '\0'; // Reject the input
        }

        // Check if the added character is alphanumeric
        if (!char.IsLetterOrDigit(addedChar))
        {
            // Display warning text
            _warningText.text = "Only alphanumeric characters (a-z, A-Z, 0-9) allowed.";
            return '\0'; // Reject the input
        }

        // Clear the warning text if the input is valid
        _warningText.text = "";

        // Accept the input
        return addedChar;
    }

    /* Input Value won't add unaccepted values.
     * However, it does not reset warning text on delete.
     * This ensures that warning text is cleared when deleting input.
     */
     
    private void OnInputValueChanged(string newValue)
    {
        // Clear the warning text when the input field value changes
        _warningText.text = "";
    }
}

