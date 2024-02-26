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
        // PlayerPrefs.SetString("CharacterName", "");

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

            
            // Start a coroutine to wait until the animation is complete.
            StartCoroutine(WaitForAnimation());

            /* 
                When executing coroutine it doesn't wait for it to finish.
                Code after line above will execute immediately after the coroutine starts, 
                without waiting for the animation to complete.
                Hence, supposed code are placed in WaitForAnimation().
            */
                
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }

    private IEnumerator WaitForAnimation() {
        
        // Wait until the end of the current frame
        yield return new WaitForEndOfFrame();

        // Wait until the specified animation is complete
        yield return new WaitForSeconds(_mainMenuAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        

        // Check if player has customized their character
        if (!IsCharacterSelected() || !IsCharacterNameSet()) { 
            OpenCharCustomization();
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
        return PlayerPrefs.HasKey("CharacterName") && !string.IsNullOrEmpty(PlayerPrefs.GetString("CharacterName"));
    }

    public bool IsCharacterSelected() {
        return PlayerPrefs.HasKey("SelectedSkinIndex") && PlayerPrefs.GetInt("SelectedSkinIndex") > -1;
    }


}
