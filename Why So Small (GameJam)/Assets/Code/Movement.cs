using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask CanJump;
    public LayerMask Walls;
    public Transform groundCheck;
    public Animator animator;
    public AudioManager audioManager;
    public HealthBar healthBar;
    public Animator deathscreenAnim;
    public Canvas canvas;
    public Transform WallCheck;



    public float speed;
    
    public float JumpForce = 4f;

    public float increaseJumpForce = 0.5f;
    


    public float respawnTime = 2f;
    public float ScaleDeath = 0.3f;

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
    private bool Flipped;

    private bool touchingWall = false;


    //Wall
    bool TouchingWall;
    public float WallSlidingSpeed = 1f;
    public float CheckRadius = 4f;
    public float CheckDecrease = 0.7f;
    public float WallRadius = 0.5f;
    public float WallDecrease = 0.1f;
    private bool wallSliding;




    void Start ()
    {
        scale = transform.localScale;
        OriginalCheckRadius = CheckRadius;
        OriginalTimerForGroundCheck = TimerForGroundCheck;
        healthBar.SetMinHealth(ScaleDeath);
        healthBar.SetMaxHealth(1);
    }

    private void Awake ()
    {
        this.gameObject.SetActive(true);
    }
    void Update ()
    {
        #region Dying

        if ( Mathf.Abs(scale.x) < ScaleDeath )
        {
            if ( PlayerDied == true)
            {
                StartCoroutine(Died());
                
            }
            PlayerDied = false;
           
            return;
        }

        #endregion


        moveInput = Input.GetAxisRaw("Horizontal");

        grounded = Physics2D.OverlapCircle(groundCheck.position , CheckRadius , CanJump);


        touchingWall = Physics2D.OverlapCircle(WallCheck.position , WallRadius , CanJump); 
        if ( touchingWall )
        {
            wallSliding = true;
            Debug.Log(wallSliding);
        }
        else
        {
            wallSliding = false;
        }
        if (wallSliding && moveInput != 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x , Mathf.Clamp(rb.velocity.y , -WallSlidingSpeed , float.MaxValue));
        }

        
        
        
        
        if ( !grounded )
        {
            LeftGround = true;

            animator.SetBool("Jumped" , true);

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
            healthBar.SetHealth(scale.y);
            OriginalCheckRadius = WallRadius;
            WallRadius -= WallDecrease;
            CheckRadius -= CheckDecrease;
            WallRadius = Mathf.Clamp(WallRadius, 0.1f , Mathf.Infinity);
            CheckRadius = Mathf.Clamp(CheckRadius , 0.5f , Mathf.Infinity);

            transform.localScale = scale;
            JumpForce += increaseJumpForce;
            
        }
        if (Input.GetButtonDown("Jump") && touchingWall )
        {
            rb.velocity = Vector2.up * JumpForce;
            animator.SetBool("Jumped" , true);
            if ( !grounded )
            {
                if ( scale.x < 0 )
                {
                    scale.x += xDecrease;
                }
                else
                {
                    scale.x -= xDecrease;
                }
                scale.y -= yDecrease;
            }
            OriginalCheckRadius = WallRadius;
            WallRadius -= WallDecrease;
            CheckRadius -= CheckDecrease;
            WallRadius = Mathf.Clamp(WallRadius , 0.1f , Mathf.Infinity);
            CheckRadius = Mathf.Clamp(CheckRadius , 0.5f , Mathf.Infinity);
            healthBar.SetHealth(scale.y);
            transform.localScale = scale;
            JumpForce += increaseJumpForce;
        }


        
        if (moveInput > 0.6f )
        {
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            Flipped = false;
        }
        if (moveInput < -0.6f && !Flipped)
        {
            scale.x = -scale.x;
            transform.localScale = scale;
            Flipped = true;
        }

        animator.SetFloat("VelocityY" , rb.velocity.y);

        //if (rb.velocity.x < 0.04f && facingRight )
        //{
        //    Flip();
        //}
        //if (rb.velocity.x > 0.04f && !facingRight)
        //{
        //    Flip();
        //}
    }




    public IEnumerator Died ()
    {
        animator.SetBool("Died", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        Debug.Log("U died");
        DeathScreen.RandomText();
        deathText.enabled = true;
        deathscreenAnim.SetBool("PlayerDied" , true);


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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(WallCheck.position , WallRadius);
    }


    void Flip ()
    {
        facingRight = !facingRight;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
