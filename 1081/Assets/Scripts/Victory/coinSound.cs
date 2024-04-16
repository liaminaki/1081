using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSound : MonoBehaviour
{
    public AudioSource aud;

    public void PlaySound(){
        aud.Play();
    }
}
