using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject  _mainMenuAnim;
    [SerializeField] private Canvas _charCustomCanvas;
    [SerializeField] private GameObject _charSelectionPanel, _charNamingPanel, _skinCards, _selectedSkinInShop;
    
    public void Start() {
        // PlayerPrefs.SetInt("SelectedSkinIndex", -1); // For testing character selection, remove in final

        _charCustomCanvas.enabled = false;
        _skinCards.SetActive(false);
        _selectedSkinInShop.SetActive(false);
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
            
            if (IsCharacterSelected() && IsCharacterNameSet())
                OpenCharCustomization();
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }


    public void OpenCharCustomization () {

        // Display Character Customization
        _charCustomCanvas.enabled = true;

        // Activate Skin Card Panel
        _skinCards.SetActive(true);

        // Deactivate Character naming panel
        _charNamingPanel.SetActive(false);

        // Activate Character selection panel 
        _charSelectionPanel.SetActive(true);

    }
    
    public bool IsCharacterNameSet() {
        return PlayerPrefs.HasKey("CharacterName") && PlayerPrefs.GetString("CharacterName") != null;
    }

    public bool IsCharacterSelected() {
        return PlayerPrefs.HasKey("SelectedSkinIndex") && PlayerPrefs.GetInt("SelectedSkinIndex") == -1;
    }


}
