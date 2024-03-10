using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private UnityEngine.UI.Button _buttonComponent;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Color _activeColor, _inactiveColor;
    [SerializeField] private Image _img;
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private Sprite _default, _pressed, _onHover;
    [SerializeField] private AudioClip _defaultClip, _pressedClip, _onHoverClip;
    [SerializeField] private AudioSource _source;

    private RectTransform textTransform, iconTransform;
    private bool onPointerDown = false, onPointerExit = false, active = true;
    private float moveDistance = 8f;

    private void Start() {
        _buttonComponent = GetComponent<UnityEngine.UI.Button>();
        
        if (_buttonText != null)
            textTransform = _buttonText.GetComponent<RectTransform>(); 

        if (_icon != null)
            iconTransform = _icon.GetComponent<RectTransform>(); 

    }

    public void OnPointerDown(PointerEventData eventData) {
        
        if(active) {

            onPointerDown = true;

            _img.sprite = _pressed;

            if (_pressedClip != null)
                _source.PlayOneShot(_pressedClip);
            
            // Move the text down
            if (_buttonText != null)
                textTransform.localPosition -= new Vector3(0f, moveDistance, 0f);
            
            if (_icon != null)
                iconTransform.localPosition -= new Vector3(0f, moveDistance/2, 0f);

        }

    }

    public void OnPointerEnter(PointerEventData eventData) {

        if (active) {

            onPointerExit = false;

            _img.sprite = _onHover;

            if (_pressedClip != _onHoverClip)
                _source.PlayOneShot(_onHoverClip);

        }

    }

    public void OnPointerUp(PointerEventData eventData) {

        if (active) {

            onPointerDown = false;

            _img.sprite = onPointerExit ? _default : _onHover;

            // Move the text back up
            if (_buttonText != null)
                textTransform.localPosition += new Vector3(0f, moveDistance, 0f);
            
            if (_icon != null)
                iconTransform.localPosition += new Vector3(0f, moveDistance/2, 0f);

        }

    }

    public void OnPointerExit(PointerEventData eventData) {

        if (active) {
            onPointerExit = true;

            // Ensure that img remains the same on pointer exit while on pointer down
            _img.sprite = !onPointerDown ? _default : _img.sprite; 

            if (_defaultClip != null)
                _source.PlayOneShot(_defaultClip);
        
        }

    }

    public void Activate() {
        
        active = true;

        if (_buttonText != null)
            _buttonText.color = _activeColor;
        
        // _buttonComponent.enabled = true;

    }

    public void Deactivate() {
       
        active = false;

        if (_buttonText != null)
            _buttonText.color = _inactiveColor;
        
        // _buttonComponent.enabled = false;

    }

    public bool IsActive() {

        return active;
    }

    public void SetInteractable(bool boolean) {

        _buttonComponent.interactable = boolean;

    }

}
