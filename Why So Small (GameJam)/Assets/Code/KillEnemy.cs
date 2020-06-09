using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public Enemy Enemy;
    public float DamageTaken = 0.4f;
    public Animator animator;
    public AudioManager AudioManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D ( Collision2D collision )
    {
        if ( collision.collider.CompareTag("Player") )
        {
            Debug.Log("The enemy died");
            animator.SetBool("Died" , true);
            AudioManager.Play("EnemyHurt");
        }
        
        
    }
}
