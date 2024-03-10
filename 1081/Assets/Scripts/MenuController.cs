using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{   
    [SerializeField] private Animator _mainMenuAnimator;
    [SerializeField] private Canvas _charCustomCanvas;
    [SerializeField] private GameObject _charSelectionPanel, _charNamingPanel, _skinCards, _selectedSkinInShop;
    
    public void Start() {
        // PlayerPrefs.SetInt("SelectedSkinIndex", -1); // For testing character selection, remove in final
        // PlayerPrefs.SetString("CharacterName", "");

        // Play splash screen if not from chapter selection
        if (SceneStateManager.PreviousScene != "ChapterSelectionScene") {
            _mainMenuAnimator.Play("SplashScreen");
        }

        // Clear the previous scene information to prevent it from persisting across scenes
        SceneStateManager.PreviousScene = null;

        // Canvas states initializations
        _charCustomCanvas.enabled = false;
        _skinCards.SetActive(false);
        _selectedSkinInShop.SetActive(false);
    }

    // Play to right transition
    public void ToRight() {
        
        if (_mainMenuAnimator != null) {
            _mainMenuAnimator.Play("ToRight");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    
    }

    // Play to left transition
    public void ToLeft() {
        
        if (_mainMenuAnimator != null) {
            
            _mainMenuAnimator.Play("ToLeft");
            Debug.Log("Played to left!");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }

    public void Play() {

        if (_mainMenuAnimator != null) {

            // Set main menu as previous scene
            SceneStateManager.PreviousScene = "MainMenuScene";
            
            // Play the specified animation
            _mainMenuAnimator.Play("ToChapterSelection");

            
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
        yield return new WaitForSeconds(_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).length);
        

        // Check if player has customized their character
        if (!IsCharacterSelected() || !IsCharacterNameSet()) { 
            OpenCharCustomization();
        }

        GoToChapterSelection();
        
    }

    public void GoToChapterSelection () {

        SceneManager.LoadScene("ChapterSelectionScene");

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
