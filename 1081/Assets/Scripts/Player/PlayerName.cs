using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerName : MonoBehaviour {
    [SerializeField] private TMP_Text _playerName;

    public void Start() {
        
        LoadName();
        
    }

    public void Update () {

        LoadName();
        
    }

    public void LoadName() {
       
       if (PlayerPrefs.HasKey("CharacterName"))
        {
            string savedName = PlayerPrefs.GetString("CharacterName");
            _playerName.text = savedName;
        }

    }

}