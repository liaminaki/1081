using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostExposureController : MonoBehaviour
{
    public Volume globalVolume;
    public Slider postExposureSlider;

    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        // Check if the Volume has a ColorAdjustments component
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            // Add a listener to the slider's value change event
            postExposureSlider.onValueChanged.AddListener(ChangePostExposure);
            Debug.Log("Color Adjustments found.");
        }
        else
        {
            Debug.LogError("Color Adjustments not found in the Global Volume!");
        }

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", -0.5f); // Default value 1.0f if not found
        postExposureSlider.value = savedBrightness;

        // Apply the initial brightness value
        ChangePostExposure(savedBrightness);

    }

    private void ChangePostExposure(float value)
    {
        // Adjust the post-exposure based on the slider value
        if (colorAdjustments != null)
        {
            // Debug.Log("yo?");
            // Debug.Log("Value is "+ value);
            colorAdjustments.postExposure.Override(value);
            SaveBrightness();
        }
    }

    public void SaveBrightness()
    {
        float brightnessValue = postExposureSlider.value;
        PlayerPrefs.SetFloat("Brightness", brightnessValue);
        PlayerPrefs.Save();
    }

    
}
