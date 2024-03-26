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
}