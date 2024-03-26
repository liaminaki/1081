using UnityEngine;

public class CutscenePlayerManager : MonoBehaviour
{
    [Header("Player Skin")]
    public GameObject FemaleSkin; // Reference to the female skin GameObject
    public GameObject MaleSkin;   // Reference to the male skin GameObject

    void Start()
    {
        // Load the selected skin index from PlayerPrefs, defaulting to 0 if not found
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);

        // Activate the appropriate skin based on the selected skin index
        if (selectedSkinIndex == 0)
        {
            ActivateFemaleSkin();
        }
        else
        {
            ActivateMaleSkin();
        }
    }

    void ActivateFemaleSkin()
    {
        // Activate the female skin GameObject and deactivate the male skin GameObject
        FemaleSkin.SetActive(true);
        MaleSkin.SetActive(false);
    }

    void ActivateMaleSkin()
    {
        // Activate the male skin GameObject and deactivate the female skin GameObject
        MaleSkin.SetActive(true);
        FemaleSkin.SetActive(false);
    }
}
