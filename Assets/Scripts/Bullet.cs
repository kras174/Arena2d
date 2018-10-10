using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 30;
    public Rigidbody2D rb;

    public GameObject imapctEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        PlayerHeart plasyer = collision.GetComponent<PlayerHeart>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (plasyer != null)
        {
            plasyer.PlayerHurt();
        }

        Instantiate(imapctEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
	