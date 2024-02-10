using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed, _onHover;
    [SerializeField] private AudioClip _defaultClip, _pressedClip, _onHoverClip;
    [SerializeField] private AudioSource _source;
    private RectTransform textTransform;
    private bool onPointerDown = false;
    private bool onPointerExit = false;

    private void Start() {

        if (_buttonText != null)
            textTransform = _buttonText.GetComponent<RectTransform>(); 
    }

    public void OnPointerDown(PointerEventData eventData) {
        onPointerDown = true;

        _img.sprite = _pressed;
        _source.PlayOneShot(_pressedClip);
        
        // Move the text down
        if (_buttonText != null)
            textTransform.localPosition -= new Vector3(0f, 8f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        onPointerExit = false;

        _img.sprite = _onHover;
        _source.PlayOneShot(_onHoverClip);
    }

    public void OnPointerUp(PointerEventData eventData) {
        onPointerDown = false;

        _img.sprite = onPointerExit ? _default : _onHover;

        // Move the text back up
        if (_buttonText != null)
            textTransform.localPosition += new Vector3(0f, 8f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        onPointerExit = true;

        // Ensure that img remains the same on pointer exit while on pointer down
        _img.sprite = !onPointerDown ? _default : _img.sprite; 

        _source.PlayOneShot(_defaultClip);
    }

}
