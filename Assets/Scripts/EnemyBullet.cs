using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Debug.Log("bullet has hit");
            HealthManager playerHealth = collision.GetComponent<HealthManager>();
            if(playerHealth != null)
            {
                Debug.Log("helath not null?");
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
