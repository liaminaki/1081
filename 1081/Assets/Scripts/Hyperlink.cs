using UnityEngine;
using System.Collections;
public class Hyperlink : MonoBehaviour {
    public void OpenURL(string link) {
        Application.OpenURL(link);
    }
}