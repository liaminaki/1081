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
        GameObject player = Instantiate(playerPrefabs[characterIndex]);
        Vcam.m_Follow = player.transform;
        
    }
}
