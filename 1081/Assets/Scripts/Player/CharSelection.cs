using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharSelection : MonoBehaviour {
    [SerializeField] private GameObject  _moveSkinCardAnim;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Button _nextButton;
    [SerializeField] private GameObject  _charNamingPanel, _charSelectionPanel; 
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private GameObject _skinCard;

    private Skin selectedSkin; 
    private int selectedSkinIndex = -1;


    public void Start() {

        // Set original position of skin cards
        MoveSelectedSkin(0f);

        // Initialize warning text to empty
        _warningText.text = "";

        ActivateAllSkins();

        LoadSkin();
        UpdateSkins();

        Debug.Log("Selected Skin Index: " + selectedSkinIndex);

        if (selectedSkin == null) {
            _nextButton.Deactivate();
            Debug.LogWarning("Next button deactivated.");
        }

    }

    public void AddToList(Skin skin) {

        if (_skins == null) {
            _skins = new List<Skin>();
        }

        _skins.Add(skin);
    }

    public void OnSkinEnter(Skin skin) {
        UpdateSkins();
        if (selectedSkin == null || skin != selectedSkin) {
            skin.SetHoverImage();
        }

    }

    public void OnSkinExit(Skin skin) {
        UpdateSkins();

    }

    public void OnSkinSelected(Skin skin) {
        selectedSkin = skin;
        selectedSkinIndex = _skins.IndexOf(skin);

        _nextButton.Activate();
        skin.SetSelectedImage();
        
        UpdateSkins(); 
        SaveSkin(); // Decide whether skin should be saved after selecting or after clicking Next button

        // Reset warning text
        _warningText.text = "";

    }

    public void UpdateSkins() {
        foreach (Skin skin in _skins) {

            if (selectedSkin != null && skin == selectedSkin) { continue; }
            skin.SetDefaultImage();
        }

    }

    public void SaveSkin() {
        
        PlayerPrefs.SetInt("SelectedSkinIndex", selectedSkinIndex);
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex");

    }

    public void LoadSkin() {
       
        if (PlayerPrefs.HasKey("SelectedSkinIndex"))
        {
            selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex");
            if (selectedSkinIndex >= 0 && selectedSkinIndex < _skins.Count)
            {
                selectedSkin = _skins[selectedSkinIndex];
                OnSkinSelected(selectedSkin);
            }
        }

    }

    public void ActivateAllSkins() {
        foreach (Skin skin in _skins) {
            skin.Activate();
        }

    }

    public void DeactivateUnselectedSkin() {
        
        foreach (Skin skin in _skins) {

            if (selectedSkin != null && skin != selectedSkin) {
                skin.Deactivate();
            }
        }

    }

    public void OnNextButtonClick() {
            
        if (selectedSkinIndex != -1) {
            
            DeactivateUnselectedSkin();
        
            // Move selected skin card
            MoveSelectedSkin(-100f);
            
            // Deactivate Character selection panel 
            _charSelectionPanel.SetActive(false);

            // Activate Character naming panel
            _charNamingPanel.SetActive(true);

        }
        
        else {

            _warningText.text = "Please select a character.";

        }
            
    }

    public void MoveSelectedSkin(float newXPosition) {

        // Check if the skin card is assigned
        if (_skinCard != null) {
            
            // Get the RectTransform component of the skin card
            RectTransform skinCardTransform = _skinCard.GetComponent<RectTransform>();
            
            if (skinCardTransform != null) {
                // Get the current position of the RectTransform
                Vector3 currentPosition = skinCardTransform.anchoredPosition;

                // Set the new X position
                currentPosition.x = newXPosition;

                // Apply the new position to the RectTransform
                skinCardTransform.anchoredPosition = currentPosition;

                Debug.Log("Moved "+ newXPosition);
            }
            
            else {
                Debug.LogError("Skin card does not have a RectTransform component!");
            }
        
        }
        
        else {
            Debug.LogError("Skin card is not assigned!");
        }

    }

}