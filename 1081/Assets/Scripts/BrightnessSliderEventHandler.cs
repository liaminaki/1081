using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessSliderEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PostExposureController brightnessController;

    // Assign this method to the slider's OnValueChanged event in the Unity Editor
    public void OnBrightnessValueChanged(float value)
    {
        brightnessController.SaveBrightness();
    }
}
