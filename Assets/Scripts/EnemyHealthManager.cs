using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public AudioClip explodeSound;
    public float healthAmount = 100f;
    public GameObject enemyexplodePrefab;
    private AudioSource explodesoundSource;

    private void Start()
    {
        //need to get audio component so i can change volume later
        explodesoundSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        //Debug.Log("Enemy Health: " + healthAmount);

        if(healthAmount <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Instantiate(enemyexplodePrefab, transform.position, Quaternion.identity);
        
        //create a temporary GameObject to play the sound
        //needed to do this because destroy() was preventing
        //death sound from being made. Then, because its a new
        //audio source, I had to make a new one to control the volume
        //because the new audio source doesnt access the audio components volume control
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = explodeSound;
        audioSource.volume = explodesoundSource.volume; //had to add this because
        audioSource.Play();

        //destroy the temp GameObject after the sound has played
        Destroy(tempAudio, explodeSound.length);
        Destroy(gameObject);

    }
    
}
