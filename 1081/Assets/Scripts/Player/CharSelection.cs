using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour {
    [SerializeField] private GameObject  _moveSkinCardAnim;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Button _nextButton;

    private Skin selectedSkin; 
    private int selectedSkinIndex;


    private void Start() {
        PlayerPrefs.SetInt("SelectedSkinIndex", -1);

        ActivateAllSkins();

        LoadSkin();
        UpdateSkins();

        Debug.Log("Selected Skin Index: " + selectedSkinIndex);

        if (selectedSkin == null) {
            _nextButton.Deactivate();
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
        DeactivateUnselectedSkin();
        _moveSkinCardAnim.GetComponent<Animator>().Play("MoveSkinCardToLeft");
    }

}