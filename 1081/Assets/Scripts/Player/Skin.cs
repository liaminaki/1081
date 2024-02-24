using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField] private GameObject _skinReference; // Reference to the GameObject representing the skin
    [SerializeField] private CharSelection _skins; 
    [SerializeField] private SpriteRenderer _skinRenderer;
    [SerializeField] private Sprite _default, _hover, _selected;

    void Start() {
        _skins.AddToList(this);
    }

    public void OnPointerClick(PointerEventData eventData) {
        _skins.OnSkinSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _skins.OnSkinEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _skins.OnSkinExit(this);
    }

    // public Image GetImage() {
    //     return this._img;
    // }

    public void SetDefaultImage() {
        _skinRenderer.sprite = _default;
    }

    public void SetHoverImage() {
        _skinRenderer.sprite = _hover;
    }

    public void SetSelectedImage() {
        _skinRenderer.sprite = _selected;
    }

    public void Activate() {
        _skinReference.SetActive(true);
    }

    public void Deactivate() {
        _skinReference.SetActive(false);
    }


   
}
