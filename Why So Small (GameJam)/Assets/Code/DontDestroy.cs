using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public AudioSource source;
    public AudioManager AudioManager;
    private bool Muted = false;
    private float Volume;
    void Start()
    {
        
        Volume = source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.M)){
            Muted = !Muted;
        }
        if ( Muted )
        {
            source.volume = 0f;
            AudioManager.enabled = false;
            
            foreach ( Sound s in AudioManager.sounds )
            {
                s.audioSource.volume = 0f;
            }

        } else if ( !Muted )
        {
            source.volume = Volume;
            foreach ( Sound s in AudioManager.sounds )
            {
                s.audioSource.volume = s.Volume;
            }
        }
    }

    private void OnLevelWasLoaded ( int level )
    {
        if (level == SceneManager.GetActiveScene().buildIndex + 1 )
        {
            SceneManager.MoveGameObjectToScene(this.gameObject , SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
}
