using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float DelayBeforeNextLevel = 1f;
    public AudioManager AudioManager;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Play("Portal");
            Movement script = collision.GetComponent<Movement>();
            rb = collision.GetComponent<Rigidbody2D>();
            
            script.enabled = false;
            StartCoroutine(LoadNextScene(1));
            
        }
    }

    IEnumerator LoadNextScene (int lol)
    {
        yield return new WaitForSeconds(DelayBeforeNextLevel - 0.8f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(DelayBeforeNextLevel - 0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
