using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharNaming : MonoBehaviour {
    private string playerPrefsKey = "CharacterName";
    private int characterLimit = 20;
    [SerializeField] private Button _backButton, _doneButton;
    [SerializeField] private GameObject  _moveSkinCardAnim;  
    [SerializeField] private GameObject  _charNamingPanel, _charSelectionPanel, _skinCards;
    [SerializeField] private SelectedSkin _selectedSkinInShop; 
    [SerializeField] private Canvas _charCustomCanvas, _shopCanvas;
    [SerializeField] private CharSelection _skins;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _warningText;
    
    public void Start() {

        // _selectedSkin.LoadSkin();
        
        if (_inputField != null && _warningText != null) {   
            
            LoadInputText();

            // Add a listener to the onValidateInput event of the input field
            _inputField.onValidateInput += ValidateInput;

            // Add a listener to the onValueChanged event of the input field
            _inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
        
        else {
            Debug.LogError("InputField or WarningText is not assigned in the inspector.");
        }

        Debug.Log("New");
    }

    public void Update() {
        
        // _selectedSkin.LoadSkin();
        ControlButtons();

    }

    private char ValidateInput(string text, int charIndex, char addedChar) {
        
        // Check if the input exceeds the character limit
        if (text.Length + 1 > characterLimit) {
            // Display warning text
            _warningText.text = "Maximum of 20 characters only.";
            return '\0'; // Reject the input
        }

        // Check if the added character is alphanumeric
        if (!char.IsLetterOrDigit(addedChar)) {
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
     
    private void OnInputValueChanged(string newName) {
        // Clear the warning text when the input field value changes
        _warningText.text = "";

        ControlButtons();
        
        // Automatically save input 
        PlayerPrefs.SetString(playerPrefsKey, newName);

    }


    // Load the saved input text from PlayerPrefs
    private void LoadInputText() {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            string savedName = PlayerPrefs.GetString(playerPrefsKey);
            _inputField.text = savedName;
        }
    }

    public void OnBackButtonClick() {

        // Check if input field is empty
        if (IsInputFieldEmpty()) {

            _warningText.text = "Name required.";

        }

        else {
            
            // Display all skin options
            _skins.ActivateAllSkins();

            // Move skin card to original position in character selection
            // _moveSkinCardAnim.GetComponent<Animator>().Play("MoveSkinCardBackToOrig");
            MoveSelectedSkin(0f);
            
            // Deactivate Character naming panel
            _charNamingPanel.SetActive(false);

            // Activate Character selection panel 
            _charSelectionPanel.SetActive(true);

        }
        
    }

    public bool IsInputFieldEmpty() {
        return _inputField != null && string.IsNullOrEmpty(_inputField.text);
    }

    public void ControlButtons() {
            
        // Check if input field is empty
        if (IsInputFieldEmpty()) {

            // Deactivate back button
            _backButton.Deactivate();

            // Deactivate Done button
            _doneButton.Deactivate();
            // _doneButton.SetInteractable(false);

        }

        else {
            // Activate back button
            _backButton.Activate();

            // Activate Done button 
            _doneButton.Activate();
            // _doneButton.SetInteractable(true);

        }

    }

    public void OnDoneButtonClick() {

        if (IsInputFieldEmpty()) {
            _warningText.text = "Name required.";
        }

        else {

            _charCustomCanvas.enabled = false;
            _skinCards.SetActive(false);

            if (SceneStateManager.PreviousScene == "MainMenuScene") {
                GoToChapterSelection();
            }

            else {
                Debug.Log(_shopCanvas.name + " Abled");
                _shopCanvas.enabled = true;
                _selectedSkinInShop.Activate();
            }
            
        }

    }

    public void MoveSelectedSkin(float newXPosition) {

        _skins.MoveSelectedSkin(newXPosition);

    }

    public void GoToChapterSelection () {

        SceneManager.LoadScene("ChapterSelectionScene");

    }

}