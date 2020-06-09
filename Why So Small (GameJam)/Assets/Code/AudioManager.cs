using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    
    void Awake ()
    {
        foreach (Sound s in sounds )
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.playOnAwake = false;
            s.audioSource.clip = s.clip;
            
            s.audioSource.volume = s.Volume;
            s.audioSource.pitch = s.Pitch;
            s.audioSource.loop = s.loop;

            AudioMixer audioMixer = s.mixer;
            
        }
    }

    // Update is called once per frame
    public void Play ( string name)
    {
        Sound s = Array.Find(sounds , Sound => Sound.name == name);
        if (s.audioSource != null )
        {
            s.audioSource.Play();
        }
    }
}
