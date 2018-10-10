using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public GameObject enemyDeath;
    public int scoreValue = 1;

    public bool friendly = false;
    public bool noScore = false;
    public bool noWaver = false;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!noScore)
            ScoreUI.score += scoreValue;
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
