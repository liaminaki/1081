using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject _panelToControl;

    // public void Start() {
    //     Deactivate(_panelToControl);
    // }
    
    // Method to enable the panel
    public void Activate() {
        if (_panelToControl != null)
        {
            _panelToControl.SetActive(true);
            Debug.Log(_panelToControl.name + " Panel Activated");
        }
    }

    // Method to disable the panel
    public void Deactivate()
    {
        if (_panelToControl != null)
        {
            _panelToControl.SetActive(false);
            Debug.Log(_panelToControl.name + " Panel Deactivated");
        }
    }

}