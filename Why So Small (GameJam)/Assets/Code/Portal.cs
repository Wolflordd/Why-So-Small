using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float DelayBeforeNextLevel = 1f;
    public AudioManager AudioManager;
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Play("Portal");
            Movement script = collision.GetComponent<Movement>();
            script.enabled = false;
            StartCoroutine(LoadNextScene(1));
            
        }
    }

    IEnumerator LoadNextScene (int lol)
    {
        yield return new WaitForSeconds(DelayBeforeNextLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
