using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask CanJump;
    public Transform groundCheck;
    public Animator animator;
    public AudioManager audioManager;
    public Animator deathscreenAnim;
    public Canvas canvas;



    public float speed;
    
    public float JumpForce = 4f;

    public float increaseJumpForce = 0.5f;
    
    public float CheckRadius = 4f;

    public float respawnTime = 2f;

    [Header("Scale")]

    public float xDecrease = 1f;
    public float yDecrease = 1f;


    private float moveInput;
    private bool grounded;
    public Vector3 scale;
    public float CheckRadiusMinus = -0.32f;
    private bool facingRight = true;
    private bool LeftGround;
    private float OriginalCheckRadius;
    private bool PlayerDied = true;
    public float TimerForGroundCheck = 0.5f;
    private float OriginalTimerForGroundCheck;
    private bool CheckIsDecreased = false;
    public DeathScreen DeathScreen;
    public Text deathText;
    void Start ()
    {
        scale = transform.localScale;
        OriginalCheckRadius = CheckRadius;
        OriginalTimerForGroundCheck = TimerForGroundCheck;
        
    }

    private void Awake ()
    {
        this.gameObject.SetActive(true);
    }
    void Update ()
    {
        if (Mathf.Abs(scale.x) < 0.3f )
        {
            if ( PlayerDied == true)
            {
                Debug.Log("U died");
                DeathScreen.RandomText();
                deathText.enabled = true;
                deathscreenAnim.SetBool("PlayerDied" , true);
                
            }
            PlayerDied = false;
            StartCoroutine(Died());
           
            return;
        }
        moveInput = Input.GetAxisRaw("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position , CheckRadius , CanJump);
        
        if ( !grounded )
        {
            LeftGround = true;

            if ( TimerForGroundCheck <= 0 )
            {
                CheckRadius = OriginalCheckRadius;
            }
            else if ( CheckIsDecreased == false)
            {
                CheckRadius += CheckRadiusMinus;
                CheckIsDecreased = true;
            }
            
            TimerForGroundCheck -= Time.deltaTime;
            
            Debug.Log(TimerForGroundCheck);
        }
        
        if ( grounded )
        {
            CheckRadius = OriginalCheckRadius;
            CheckIsDecreased = false;
            TimerForGroundCheck = OriginalTimerForGroundCheck;
            if ( LeftGround )
            {
                audioManager.Play("HitGround");
            }
            LeftGround = false;
            animator.SetBool("Jumped" , false);
           
        }
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            
            
            audioManager.Play("Jump");
            rb.velocity = Vector2.up * JumpForce;
            animator.SetBool("Jumped" , true);
            if (scale.x < 0 )
            {
                scale.x += xDecrease;
            }
            else
            {
                scale.x -= xDecrease;
            }
            scale.y -= yDecrease;
            transform.localScale = scale;
            JumpForce += increaseJumpForce;
            
        }





        animator.SetFloat("VelocityY" , rb.velocity.y);

        if (rb.velocity.x < 0 && facingRight )
        {
            Flip();
        }
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    IEnumerator Died ()
    {
        animator.SetBool("Died", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        
        
        
        yield return new WaitForSeconds(respawnTime);
        deathText.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void FixedUpdate ()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetInteger("Speed" , Mathf.RoundToInt(rb.velocity.x));
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position , CheckRadius);
    }


    void Flip ()
    {
        facingRight = !facingRight;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
