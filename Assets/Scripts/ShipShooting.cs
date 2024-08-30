using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShipShooting : MonoBehaviour
{
    public GameObject BulletPrefab;      
    public Transform firePoint;          // fire point is where bulle is being launched from 
    public float bulletSpeed = 10f;      
    public int maxBullets = 10;          // usable bullets before reload
    private int currentBullets;          // current bullets available
    public float reloadTime = 2f;        // time it takes to reload
    private bool isReloading = false;    // whether the ship is currently reloading
    public float fireRate = .2f;
    private float nextFireTime = 0f;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public TextMeshProUGUI bulletCounterText;
    private AudioSource shootsoundSource;
    private AudioSource reloadsoundSource;

    void Start()
    {
        currentBullets = maxBullets;     //start with full bullets and update bullet UI
        UpdateBulletUI();
        shootsoundSource = GetComponent<AudioSource>();
        reloadsoundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isReloading)
            return;

        //fire if bullets are available, and space is held down OR pressed
        if (Input.GetKey(KeyCode.Space) && currentBullets > 0 && Time.time >= nextFireTime) // Fire if bullets are available
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // eload if out of bullets
        if (currentBullets <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        // add velocity to the bullet to make it move
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D shipRb = GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = new Vector2(firePoint.right.x, firePoint.right.y) * bulletSpeed;
        rb.velocity = bulletVelocity;

        shootsoundSource.PlayOneShot(shootSound);

        currentBullets--;  
        UpdateBulletUI(); 
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadsoundSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime); //cause a delay in the reload

        currentBullets = maxBullets;  
        isReloading = false;

        UpdateBulletUI();  
    }

    void UpdateBulletUI()
    {
        string bulletTicks = "Ammo: ";
        for (int i = 0; i < currentBullets; i++)
        {
            bulletTicks += "|";  // adds tick mark for each bullet
        }

        
        bulletCounterText.text = bulletTicks;
    }
}
