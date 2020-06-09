using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    public AudioManager AudioManager;
    public AudioMixer AudioMixer;
    public AudioMixer Sfx;
    public GameObject SettingsPanel;
    public GameObject PauseMenu;
    public GameObject EscText;
    private void Start ()
    {
        
    }
    public void SetMusic(float Volume )
    {
        Debug.Log(Volume);
        AudioMixer.SetFloat("MusicVol" , Volume);
    }

    public void SetSFX(float Volume )
    {
        Sfx.SetFloat("SFXvol" , Volume);
        foreach(Sound s in AudioManager.sounds )
        {
            s.audioSource.volume = Volume;
            s.Volume = Volume;
        }
    }

    public void GoToSettings ()
    {
        PauseMenu.SetActive(false);
        SettingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoBackToPause ()
    {
        PauseMenu.SetActive(true);
        SettingsPanel.SetActive(false);    
    }
}
