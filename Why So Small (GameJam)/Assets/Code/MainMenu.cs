using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject PanelMainMenu;
    public GameObject CreditsPanel;
    public Animator Animator;
    public Animator animator;


    void Start ()
    {
        Animator.SetTrigger("Normal");
    }


    public void StartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Animator.SetTrigger("Normal");
        animator.SetTrigger("Normal");
    }

    public void Credits ()
    {
        PanelMainMenu.SetActive(false);
        Animator.SetTrigger("Normal");
        animator.SetTrigger("Normal");
        CreditsPanel.SetActive(true);
    }

    public void GoBack ()
    {
        PanelMainMenu.SetActive(true);
        Animator.SetTrigger("Normal");
        animator.SetTrigger("Normal");
        CreditsPanel.SetActive(false);
    }

}
