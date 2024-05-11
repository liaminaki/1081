using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas _canvasToControl;

    public void Start() {
        AdjustCanvasSize();
        DisableCanvas();
    }

    void AdjustCanvasSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float canvasHeight = rt.rect.height;
        float desiredCanvasWidth = canvasHeight * Camera.main.aspect;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, desiredCanvasWidth);
    }
    
    // Method to enable the canvas
    public void EnableCanvas() {
        if (_canvasToControl != null)
        {
            _canvasToControl.enabled = true;
            AdjustCanvasSize();
            Debug.Log(_canvasToControl.name + " Canvas Enabled");
        }
    }

    // Method to disable the canvas
    public void DisableCanvas()
    {
        if (_canvasToControl != null)
        {
            _canvasToControl.enabled = false;
            Debug.Log(_canvasToControl.name + " Canvas Disabled");

        }
    }

}