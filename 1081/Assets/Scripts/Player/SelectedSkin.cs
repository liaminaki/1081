using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedSkin : MonoBehaviour {
    [SerializeField] private List<Sprite> _skins;
    [SerializeField] private SpriteRenderer _skinRenderer;
    private int selectedSkinIndex;

    public void Start() {
        
        LoadSkin();
        _skinRenderer.enabled = true;
        
    }

    public void LoadSkin() {
       
        if (PlayerPrefs.HasKey("SelectedSkinIndex")) {
            selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex");
            
            if (selectedSkinIndex >= 0 && selectedSkinIndex < _skins.Count) {
                _skinRenderer.sprite = _skins[selectedSkinIndex];
                Debug.Log("Skin Changed");
            }

        }

    }

    public void Deactivate() {

        _skinRenderer.enabled = false;

    }

}