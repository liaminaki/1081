using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LastDialogueManager : MonoBehaviour
{ 
    public TextMeshProUGUI DialogueAreaCopy;
    private string language;

    void Start() {
        gameObject.SetActive(false);
        LoadSelectedLanguage();

    }

    private void LoadSelectedLanguage()
    {
        language = PlayerPrefs.GetString("Language");
    }

    public void LeaveLastPrologueLine() {
        gameObject.SetActive(true);
        switch (language)
        {
            case "ENGLISH":
                DialogueAreaCopy.text = "<color=#000000>Class, again, close your eyes as we</color> experience the past";
                break;
            case "CEBUANO":
                DialogueAreaCopy.text = "<color=#000000>Class, ipiyong na inyong mata kay atong</color> isinati ang mga kaagi";
                break;
            case "FILIPINO":
                DialogueAreaCopy.text = "<color=#000000>Class, muli, ipikit ang iyong mga mata at ating</color> danasin ang nakaraan";
                break;
        }
    }
}