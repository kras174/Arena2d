using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour {

    public int playerLives = 3;
    public int powerUpLives = 1;

    public float playerBlindingTime = 2f;
    public bool detectCollision = false;

    public Transform playerDeath;
    public Animator animator;
    public PlayerController playerController;
    public GameObject enemyPref;

    EnemyAI enemyAi;

    public void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        if (playerDeath == null)
        {
            Debug.LogError("PlayerDeath transform needed in PlayerHeart!");
        }
        if (animator == null)
        {
            Debug.LogError("Animator needed in PlayerHeart!");
        }
        if (playerController == null)
        {
            Debug.LogError("PlayerController needed in PlayerHeart!");
        }


    }

    public void Update()
    {
        LivesUI.lives = playerLives;
    }

    public void PlayerHurt()
    {
        if (!detectCollision)
        {
            playerLives--;
            LivesUI.lives = playerLives;
            detectCollision = true;
        }

        if (playerLives <= 0)
        {
            PlayerDie();
        }
        else
        {
            playerController.TriggerHurt(playerBlindingTime);
        }
    }

    void PlayerDie()
    {
        Instantiate(playerDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        BafManager baf = collision.collider.GetComponent<BafManager>();
        enemyAi = enemyPref.GetComponent<EnemyAI>();

        if (enemy != null && !enemy.friendly)
        {
            PlayerHurt();
        }
        if (baf != null && baf.bType == BafManager.bafType.HEALING)
        {
            Healing(powerUpLives);
            baf.killBaf();
        }
        if (baf != null && baf.bType == BafManager.bafType.FREEZING)
        {
            StartCoroutine(enemyAi.FreezeEnemy());
            baf.killBaf();
        }
    }

    public void Healing(int heal)
    {
        playerLives += heal;
    }
}
