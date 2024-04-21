using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class screenMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text CoinText;
    private PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        // PlayerPrefs.SetInt("PlayerCoins",200);
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = player.currentCoins.ToString();
    }
}
