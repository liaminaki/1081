using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour {

    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Button _button;

    private Skin selectedSkin; 

    private void Start() {

        if (selectedSkin == null) {
            _button.Deactivate();
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
            skin.SetSelectedImage();
        }
    }

    public void OnSkinExit(Skin skin) {
        UpdateSkins();
    }

    public void OnSkinSelected(Skin skin) {
        selectedSkin = skin;
        _button.Activate();
        UpdateSkins(); 
        skin.SetSelectedImage();

    }

    public void UpdateSkins() {
        foreach (Skin skin in _skins) {

            if (selectedSkin != null && skin == selectedSkin) { continue; }
            skin.SetDefaultImage();
        }
    }
    
}