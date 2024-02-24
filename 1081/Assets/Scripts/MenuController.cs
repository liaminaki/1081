using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject  _mainMenuAnim;
    [SerializeField] private GameObject _characterSelectionPanel;
    // [SerializeField] private GameObject _characterNamingPanel;
    [SerializeField] private PanelController _panelController;

    public void Start() {
        _panelController.Deactivate(_characterSelectionPanel);
    }

    public void ToRight() {
        
        if (_mainMenuAnim != null) {
            // Play the specified animation
            _mainMenuAnim.GetComponent<Animator>().Play("ToRight");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    
    }

    public void ToLeft() {
        
        if (_mainMenuAnim != null) {
            // Play the specified animation
            _mainMenuAnim.GetComponent<Animator>().Play("ToLeft");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }

    public void Play() {

        if (_mainMenuAnim != null) {
            // Play the specified animation
            _mainMenuAnim.GetComponent<Animator>().Play("ToChapterSelect");

            // Check if player already customized their character
            if (PlayerPrefs.HasKey("SelectedSkinIndex") && PlayerPrefs.GetInt("SelectedSkinIndex") == -1) { 
                _panelController.Activate(_characterSelectionPanel);
            }

        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }

    


}
