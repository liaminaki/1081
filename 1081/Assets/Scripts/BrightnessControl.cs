using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Slider darknessSlider;
    public Image darkOverlay;

    private void Start()
    {
        // Add a listener to the slider's value change event
        darknessSlider.onValueChanged.AddListener(ChangeDarkness);
    }

    private void ChangeDarkness(float value)
    {
        // Adjust the alpha value of the dark overlay based on the slider value
        Color overlayColor = darkOverlay.color;
        overlayColor.a = value;
        darkOverlay.color = overlayColor;
    }
}
