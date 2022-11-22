using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private AudioSource buttonAudio;

    private void Awake()
    {
        this.buttonAudio = GetComponent<AudioSource>();
    }


    public void PlayButtonAudio()
    {
        this.buttonAudio.pitch = Random.Range(0.8f, 1.2f);
        this.buttonAudio.Play();
    }
}
