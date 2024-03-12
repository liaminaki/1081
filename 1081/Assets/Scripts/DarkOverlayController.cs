using UnityEngine;
using UnityEngine.UI;

public class DarkOverlayController : MonoBehaviour
{
    public Slider brightnessSlider;
    public Image darkOverlay;

    private void Start()
    {
        // Add a listener to the slider's value change event
        brightnessSlider.onValueChanged.AddListener(ChangeDarkness);
    }

    private void ChangeDarkness(float value)
    {
        // Adjust the alpha value of the dark overlay based on the slider value
        Debug.Log("Slider value: " + value);
        Color overlayColor = darkOverlay.color;
        overlayColor.a = 1 - value; // Invert the value to control darkness
        darkOverlay.color = overlayColor;
    }
}
