using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource aud;

    public void PlaySound(){
        aud.Play();
    }
}
