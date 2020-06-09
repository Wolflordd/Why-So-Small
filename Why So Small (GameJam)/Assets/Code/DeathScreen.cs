using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Movement Movement;
    public Text text;
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RandomText ()
    {

        int ran = Random.Range(1 , 5);
        switch ( ran )
        {
            case 1:
                text.text = "Where are you? I can't see you";
                break;
            case 2:
                text.text = "Oh no! You are too small!";
                break;
            case 3:
                text.text = "Too bad, too small";
                break;
            case 4:
                text.text = "We have identified a new bacteria";
                break; 
            
            
            default:
                text.text = "Yepee, I can stomp on you!";
                break;
        }
    }
}
