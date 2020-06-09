using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform APoint;
    public Transform BPoint;
    public float speed;
    public float damage = 0.4f;
    public float RateDamage = 1f;
    public bool GoingToApoint = true;
    public float DistanceBtwPointAndTurn = 0.5f;
    public AudioManager AudioManager;
    private Vector3 dir;
    [HideInInspector]
    public Vector3 scale;
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
       


        if ( GoingToApoint )
        {
            if (scale.x < 0 )
            {
                Flip();
            }
            dir = new Vector3(transform.position.x - APoint.position.x, transform.position.y - APoint.position.y, transform.position.z - APoint.transform.position.z);
        }else if ( !GoingToApoint )
        {
            
            if (scale.x > 0 )
            {
                Flip();
            }
            dir = new Vector3(transform.position.x - BPoint.position.x , transform.position.y - BPoint.position.y , transform.position.z - BPoint.transform.position.z);
        }
        
        
        if (Mathf.Abs(dir.x) < DistanceBtwPointAndTurn )
        {
            GoingToApoint = !GoingToApoint;
        }
        else
        {
            rb.velocity = (dir.normalized * speed);
        }

        
        
        
    }


    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if ( collision.CompareTag("Player") )
        {
            Debug.Log(collision.name);
            StartCoroutine(Damage(collision));
        }
    }
    

    IEnumerator Damage(Collider2D player )
    {
        Movement script = player.gameObject.GetComponent<Movement>();


        if ( script.scale.x < 0 )
        {
            script.scale.x += damage;
        }
        else
        {
            script.scale.x -= damage;
        }
        if ( script.scale.y < 0 )
        {
            script.scale.y += damage;
        }
        else
        {
            script.scale.y -= damage;

        }

        AudioManager.Play("PlayerHurt");
        yield return new WaitForSeconds(RateDamage);
    }




    void Flip ()
    {
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
