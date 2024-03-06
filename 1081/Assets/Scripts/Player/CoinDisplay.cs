using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour {
    [SerializeField] private TMP_Text _playerCoins;

    // Start is called before the first frame update
    public void Start() {
        LoadCoins();
    }

    // Update is called once per frame
    public void Update() {
        LoadCoins();  
    }
    
    public void LoadCoins() {

        if (PlayerPrefs.HasKey("PlayerCoins"))
        {
            //get the amount of coins from PlayerPrefs
            int savedCoins = PlayerPrefs.GetInt("PlayerCoins");

            //Set the coint amount to the Text component
            _playerCoins.text = savedCoins.ToString();

        }
        else
        {
            //"PlayerCoins" key was not found, set default to 200
            int defaultCoins = 200;
            _playerCoins.text = defaultCoins.ToString();
        }
    }
}
