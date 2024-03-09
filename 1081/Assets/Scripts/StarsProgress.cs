using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarsProgress : MonoBehaviour
{
    [SerializeField] private TMP_Text _progress;

    private void Start() {
        LoadTotalStars();
    }

    public void LoadTotalStars() {
        int sum = 0;

        for(int i = 1; i <= 5; i++) {
            
            //Add the chapter 1 stars number, chapter 2 stars number.....
            sum += PlayerPrefs.GetInt("Chapter" + i + "Stars");
        }

        _progress.text = sum + " / " + 15;
    }
}