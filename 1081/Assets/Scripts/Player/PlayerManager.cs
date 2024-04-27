using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public CinemachineVirtualCamera Vcam;
    public GameObject[] playerPrefabs;
    public int characterIndex {get; private set;}
    private PlayerInput playerInput;
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
        
        // Get the PlayerInput component from the current player
        playerInput = playerPrefabs[characterIndex].GetComponent<PlayerInput>();

    }

    // Method to turn on player input
    public void TurnOnPlayerInput()
    {
        if (playerInput != null)
            playerInput.enabled = true;
    }

    // Method to turn off player input
    public void TurnOffPlayerInput()
    {
        if (playerInput != null)
            playerInput.enabled = false;
    }
}
