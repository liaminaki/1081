using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerManager : MonoBehaviour
{
    public CinemachineVirtualCamera Vcam;
    public GameObject[] playerPrefabs;
    private int characterIndex;
    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
        
         // Disable the other player prefab
        int otherIndex = (characterIndex == 0) ? 1 : 0;
        playerPrefabs[otherIndex].SetActive(false);

        // playerPrefabs[characterIndex].SetActive(true);
        
        // GameObject player = Instantiate(playerPrefabs[characterIndex]);

        if (Vcam != null)
            Vcam.m_Follow = playerPrefabs[characterIndex].transform;
        
    }
}
