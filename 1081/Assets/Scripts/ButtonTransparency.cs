using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class ButtonTransparency: MonoBehaviour {

    [SerializeField] private Image _img;

    void Start() {
        _img.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

}