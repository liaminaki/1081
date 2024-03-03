using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChapterSelection : MonoBehaviour {

    [SerializeField] private Animator _chapterSelectionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu() {
        
        SceneStateManager.PreviousScene = "ChapterSelectionScene";
        // _chapterSelectionAnimator.SetTrigger("BackToMenu");
        _chapterSelectionAnimator.Play("ToMenu");

        // Start a coroutine to wait until the animation is complete.
        StartCoroutine(WaitForAnimation());

    }

    private IEnumerator WaitForAnimation() {
        
        // Wait until the end of the current frame
        yield return new WaitForEndOfFrame();

        // Wait until the specified animation is complete
        yield return new WaitForSeconds(_chapterSelectionAnimator.GetCurrentAnimatorStateInfo(0).length);
        
        // Back to main menu
        SceneManager.LoadScene("MainMenuScene");

    }
}
