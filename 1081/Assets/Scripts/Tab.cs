using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField] private TabGroup _tabGroup; 
    private Image img;
    private TMP_Text pageTitle;

    void Start() {
        img = GetComponent<Image>();
        pageTitle = GetComponentInChildren<TMP_Text>();
        _tabGroup.AddToList(this);
    }

    public void OnPointerClick(PointerEventData eventData) {
        _tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _tabGroup.OnTabExit(this);
    }

    public Image GetImage() {
        return this.img;
    }

    public void SetImage(Sprite image) {
        img.sprite = image;
    }

    public void SetColor(Color color) {
        if (pageTitle != null) {
            pageTitle.color = color;
        }
    }

    public void SetFontAsset(TMP_FontAsset fontAsset) {
        if (pageTitle != null && fontAsset != null) {
            pageTitle.font = fontAsset;
        }
    }
   

}
