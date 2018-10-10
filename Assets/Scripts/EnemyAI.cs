using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float speed = 5f;
    public float freezeTime = 2f;
    public Rigidbody2D rb;

    float direction = 1f;
    bool facingRight = true;
    bool freezeEnemy = false;
    public Animator animator;

    Vector2 opVector;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!freezeEnemy)
        {
            animator.SetFloat("Speed", speed);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        if (direction > 0 && !facingRight)
            Flip();
        if (direction < 0 && facingRight)
            Flip();
    }

    void OnCollisionEnter2D(Collision2D colllision)
    {
        if (colllision.gameObject.tag == "Wall" || colllision.gameObject.tag == "Chest")
            direction *= -1f;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public IEnumerator FreezeEnemy()
    {
        //animator.SetFloat("Speed", 0f);
        freezeEnemy = true;
        yield return new WaitForSeconds(freezeTime);
        freezeEnemy = false;
    }
}
