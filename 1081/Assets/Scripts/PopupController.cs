using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PopupController : MonoBehaviour {

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip showSound;
    [SerializeField] private AudioClip hideSound;

    void Start() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);

        // Play the show sound effect
        if (_audioSource != null && showSound != null) {
            _audioSource.PlayOneShot(showSound);
        }
    }

    public void Hide() {
        _animator.Play("hide");

        // Play the hide sound effect
        if (_audioSource != null && hideSound != null) {
            _audioSource.PlayOneShot(hideSound);
        }
    }

    public void OffPopup() {
        gameObject.SetActive(false);
    }

}