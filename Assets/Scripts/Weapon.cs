using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float enemyFireRate = 2f;
    private float fireCountdow;

    RaycastHit2D hit;

    void Start()
    {
        fireCountdow = 0f;
    }

    void Update () {

        Enemy enemy = gameObject.GetComponent<Enemy>();

		if (Input.GetButtonDown("Fire1"))       // Player Shoot
        {
            if (enemy == null)
                Shoot();
        }
        if (enemy != null)                      // Enemy shoot
        {
            hit = Physics2D.Raycast(firePoint.position, transform.right, 5f);
            if (hit.transform && hit.transform.name == "Player")
            {
                if (fireCountdow <= 0)
                {
                    Shoot();
                    fireCountdow = enemyFireRate;
                }
                else
                {
                    fireCountdow -= Time.deltaTime;
                }
            }
        }
    }

    void Shoot ()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
