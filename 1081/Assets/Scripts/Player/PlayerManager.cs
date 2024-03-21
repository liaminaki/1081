using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    private int characterIndex;

    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
        Instantiate(playerPrefabs[characterIndex]);

    }
}
