using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public AudioMixer mixer;
    [Range(0f, 1f)]
    public float Volume;

    [Range(0.1f, 3f)]    
    public float Pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;
}
