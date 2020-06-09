using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject panelPause;
    public GameObject Player;
    public GameObject EscText;
    public GameObject SettingsPanel;
    private bool Paused = false;
    private bool pressed;
    private void Start ()
    {
        panelPause.SetActive(false);
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Paused = !Paused;
            pressed = true;
        }


        if ( Paused && pressed)
        {
            PausetheGame();
            pressed = false;
        }
        else if ( !Paused && pressed)
        {
            Resume();
            pressed = false;
        }
    }

    public void PausetheGame ()
    {
        Player.SetActive(false);
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        EscText.SetActive(false);
    }

    public void Resume ()
    {
        Player.SetActive(true);
        Time.timeScale = 1f;
        panelPause.SetActive(false);
        EscText.SetActive(true);
        SettingsPanel.SetActive(false);

    }



    public void GoToMenu ()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(0);
    }
}
