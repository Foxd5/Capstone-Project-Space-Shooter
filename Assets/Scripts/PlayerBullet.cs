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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
