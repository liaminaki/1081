using UnityEngine;
using TMPro;
public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private string language = "";

    public TextMeshProUGUI output;

    private void Start()
    {
        // Load the saved language selection if it exists
        LoadSavedLanguage();
        int savedIndex = FindLanguageIndex(language);
        
        if (savedIndex != -1)
        {
            dropdown.value = savedIndex;
        }
        
        else
        {
            // If the saved language doesn't exist in the dropdown, set the default language
            dropdown.value = 0;
            SaveLanguage("ENGLISH");
        }

        // Add listener for dropdown value change
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
        

    }

    private int FindLanguageIndex(string language)
    {
        // Find the index of the given language in the dropdown options
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == language)
            {
                return i;
            }
        }
        return -1; // Return -1 if the language is not found
    }

    private void SaveLanguage(string selectedLanguage)
    {
        // Save the default language to PlayerPrefs
        PlayerPrefs.SetString("Language", selectedLanguage);
        Debug.Log("Selected language: " + selectedLanguage);

    }

    private void LoadSavedLanguage()
    {
        language = PlayerPrefs.GetString("Language");
    }

    // Method to handle dropdown value change
    private void DropdownValueChanged(TMP_Dropdown change)
    {
        string selectedLanguage = dropdown.options[dropdown.value].text;
        
        // Save the selected language to PlayerPrefs
        SaveLanguage(selectedLanguage);

        output.text = selectedLanguage;
        
    }
    
}