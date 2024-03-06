using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerShield;
    // Start is called before the first frame update
    public void Start()
    {
        LoadShieldNumber();
    }

    // Update is called once per frame
    public void Update()
    {
        LoadShieldNumber();
    }

    public void LoadShieldNumber()
    {
        if (PlayerPrefs.HasKey("NoOfShield"))
        {
            //get the number of shield from PlayerPrefs
            int shieldNumber = PlayerPrefs.GetInt("NoOfShield");

            //set the shield number to the Text component
            _playerShield.text = "You have : " + shieldNumber.ToString();
        }
        else
        {
            //load default shield number;
            int defaultShield = 0;
            _playerShield.text = "You have : " + defaultShield.ToString();
        }
    }
}
