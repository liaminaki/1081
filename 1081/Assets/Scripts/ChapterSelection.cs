using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChapterSelection : MonoBehaviour {

    [SerializeField] private Animator _chapterSelectionAnimator;
    private int chaptersUnlocked;
    private int isNewChapterUnlocked;
    
    public GameObject JournalCanvas;
    public GameObject JournalContainer; // Parent where journal will be instantiated
    public List<GameObject> Journals;
    private GameObject journal = null; // Reference to instantiated journal

    public ChapterButton Chapter1Button;

    // Start is called before the first frame update
    void Start() {
        SetJournalCanvas(false);

        Debug.Log("Chapter selection running");
        // Play transition if not from chapter selection
        if (SceneStateManager.PreviousScene == "MainMenuScene") {
           _chapterSelectionAnimator.Play("FromMenu");
        }

        else {
            _chapterSelectionAnimator.Play("FromGame");
        }

        // Clear the previous scene information to prevent it from persisting across scenes
        if (SceneStateManager.PreviousScene != "Epilogue")
            SceneStateManager.PreviousScene = null;
        
        LoadIsNewChapterUnlocked();
        LoadChaptersUnlocked();

        if (chaptersUnlocked == 0) {
            Chapter1Button.LockChapter();

            PlayerPrefs.SetInt("ChaptersUnlocked", 1);
            PlayerPrefs.SetInt("IsNewChapterUnlocked", 1);

            LoadIsNewChapterUnlocked();
            LoadChaptersUnlocked();

            Chapter1Button.RemoveQMarkThenUnlock();

        }

        // Play strings animation when a new chapter is unlocked
        if (isNewChapterUnlocked == 1) {

            Debug.Log(chaptersUnlocked);

            _chapterSelectionAnimator.SetInteger("ChaptersUnlocked", chaptersUnlocked-1);
            _chapterSelectionAnimator.SetInteger("UnlockChapter", chaptersUnlocked);

            if (chaptersUnlocked != 2)
                // Reset isNewChapterUnlocked
                PlayerPrefs.SetInt("IsNewChapterUnlocked", 0);

        }

        else if (chaptersUnlocked > 0) {
            _chapterSelectionAnimator.SetInteger("ChaptersUnlocked", chaptersUnlocked);
        }
        
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

    // Load numbers of chapters unlocked
    private void LoadChaptersUnlocked() {
        
        chaptersUnlocked = PlayerPrefs.GetInt("ChaptersUnlocked");

    }

    private void LoadIsNewChapterUnlocked() {
        
        isNewChapterUnlocked = PlayerPrefs.GetInt("IsNewChapterUnlocked");

    }

    public void Reset() {
        // PlayerPrefs.DeleteKey("Chapter1Stars");
        // PlayerPrefs.DeleteKey("Chapter2Stars");
        // PlayerPrefs.DeleteKey("Chapter3Stars");
        // PlayerPrefs.DeleteKey("Chapter4Stars");
        // PlayerPrefs.DeleteKey("Chapter5Stars");
        // PlayerPrefs.DeleteKey("ChaptersUnlocked");
        // PlayerPrefs.DeleteKey("IsNewChapterUnlocked");
        // PlayerPrefs.DeleteKey("NewChapterUnlocked");

        PlayerPrefs.DeleteAll();
    }

    // Instantiate chapter journal in selection
    public void OpenJournal(int n) {

        SetJournalCanvas(true);
        
        // Subtract 1 since index starts with 0
        n -= 1;

        // Instantiate journal
        journal = Object.Instantiate(Journals[n], JournalContainer.transform);

    }

    public void CloseJournal() {
        Destroy(journal);
        journal = null;

        SetJournalCanvas(false);
    }

    public void SetJournalCanvas(bool boolean) {
        JournalCanvas.SetActive(boolean);
    }

    public void ShowLastJournal() {
        
        if (SceneStateManager.PreviousScene == "Epilogue") {
           OpenJournal(5);
        }

        SceneStateManager.PreviousScene = null;

    }

}
