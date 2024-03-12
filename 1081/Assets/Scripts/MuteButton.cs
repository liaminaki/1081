using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite unmuteSprite; // Reference to the unmute state image
    public Sprite muteSprite;   // Reference to the mute state image
    private Image buttonImage;
    private bool isMuted = false; // Initial state is unmuted
    public Slider volumeSlider;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonImage();
    }

    // Update is called once per frame
     public void OnButtonClick()
    {
        // Toggle the state when the button is clicked
        isMuted = !isMuted;

        // Update the button image based on the new state
        UpdateButtonImage();
        AdjustVolume();
        // Optionally, you can perform other actions here
    }

    private void UpdateButtonImage()
    {
        // Change the image based on the current state
        buttonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    private void AdjustVolume()
    {
        float targetVolume = isMuted ? -70f : 0.7f;
        volumeSlider.value = targetVolume;
        // Set the volume to 0 if muted, otherwise set it to a desirable amount (e.g., 0.7)
        AudioListener.volume = isMuted ? 0f : 0.7f;
    }
}
