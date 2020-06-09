using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string SceneToLoad;
    public Animator transitionAnim;
    public MainMenu canvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    



    public IEnumerator Transition ()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(SceneToLoad);
    }
}
