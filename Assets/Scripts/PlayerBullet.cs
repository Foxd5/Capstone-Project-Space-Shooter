using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        //player bullets were colliding with player ships. hopefully this fixes that
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        Collider2D bulletCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);

        //this next section is so that the player bullet will ignore enemy bullets.
        //subject to change later: maybe its fun to destroy enemy bullets?
        //need to change the color of enemy bullets too so its easier to see.
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject enemyBullet in enemyBullets)
        {
            Collider2D enemyBulletCollider = enemyBullet.GetComponent<Collider2D>();
            if (enemyBulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, enemyBulletCollider);
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
