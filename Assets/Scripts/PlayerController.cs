using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 25f;
    public float jumpForce = 700f;
    float move = 0f;

    private Transform groundCheck;
    private Rigidbody2D rb;
    private bool facingRight = true;

    public LayerMask playerMask;

    public bool isGrounded = false;

    private Vector3 m_Velocity = Vector3.zero;

    public Animator animator;

    public PlayerHeart playerHeart;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        playerHeart = gameObject.GetComponent<PlayerHeart>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(move));
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(rb.position, groundCheck.position, playerMask);

        Move();

    }

    void Move()
    {
        if (move > 0 && !facingRight)
            Flip();
        if (move < 0 && facingRight)
            Flip();

        Vector3 targetVelocity = new Vector2(move * Time.fixedDeltaTime * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, .05f);
    }

    void Jump()
    {
        if (isGrounded)
            rb.AddForce(new Vector2(0f, jumpForce));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
   
    public void TriggerHurt(float heartTime)
    {
        StartCoroutine (HurtBlinker(heartTime));
    }

    IEnumerator HurtBlinker(float heartTime)
    {
        //Ignore collisions with elements
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);

        //Start looping blink animation
        animator.SetLayerWeight(1, 1);

        //Wait for invincibility to end
        yield return new WaitForSeconds(heartTime);

        //Stop blinking animation and re-enable collision
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        animator.SetLayerWeight(1, 0);
        playerHeart.detectCollision = false;
    }
}
