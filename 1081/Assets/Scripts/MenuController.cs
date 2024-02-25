using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject  _mainMenuAnim;
    [SerializeField] private Canvas _charCustomCanvas;
    [SerializeField] private GameObject _charSelectionPanel, _charNamingPanel;
    
    public void Start() {
        _charCustomCanvas.enabled = false;
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
                
                // Display Character Customization
                _charCustomCanvas.enabled = true;

                // Deactivate Character naming panel
                _charNamingPanel.SetActive(false);

                // Activate Character selection panel 
                _charSelectionPanel.SetActive(true);
            }

        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }


    


}
