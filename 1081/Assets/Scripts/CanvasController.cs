using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas _canvasToControl;

    public void Start() {
        DisableCanvas();
    }
    
    // Method to enable the canvas
    public void EnableCanvas() {
        if (_canvasToControl != null)
        {
            _canvasToControl.enabled = true;
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