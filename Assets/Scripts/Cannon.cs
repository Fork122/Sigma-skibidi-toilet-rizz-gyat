using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet; 
    public Transform firePoint;
    public float interval = 2f; 
    public float speed = 10f; 
    public float bulletLifeSpan = 5f; 

    private Transform player; 
    private float time; 

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Aim(); 
            HandleShooting(); 
        }
    }

    void Aim()
    {
        //direction from cannon to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Convert to angle and align 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle+90);
    }

    void HandleShooting()
    {        time += Time.deltaTime;
        if (time >= interval && HasLineOfSight())
        {
            Fire(); 
            time = 0f;
        }
    }

    bool HasLineOfSight()
    {
        Vector2 direction = player.position - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, direction.magnitude);
    //make sure it hits the player rather than something else
        if (hit.collider != null)
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    void Fire()
    {

        if (bullet != null && firePoint != null)
        {
        
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);

            Destroy(projectile, bulletLifeSpan);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * speed;
            }
            
        }
        
    }
}

