using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoTo(string sceneName)
    {
        // Set the previous scene to the current scene before loading the new scene
        SceneStateManager.PreviousScene = SceneManager.GetActiveScene().name;

        // Load the specified scene
        SceneManager.LoadScene(sceneName);

    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void IsOldEnough()
    {
        PlayerPrefs.SetInt("IsOld", 1);
    }

    public void WatchedEpilogue()
    {
        PlayerPrefs.SetInt("HasWatchedEpilogue", 1);
    }


    public void WatchedPrologue()
    {
        PlayerPrefs.SetInt("HasWatchedPrologue", 1);
    }

}