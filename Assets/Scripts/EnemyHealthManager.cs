using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int pointValue = 10;
    public AudioClip shipexplodeSound;
    public float healthAmount = 100f;
    public GameObject enemyexplodePrefab;

    private AudioSource shipexplodesoundSource;
    private ScoreManager scoreManager;


    void Start()
    {
        //need to get audio component so i can change volume later
        AudioSource[] audioSources = GetComponents<AudioSource>();
        shipexplodesoundSource = audioSources[0];

        scoreManager = FindObjectOfType<ScoreManager>();
       
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
        if (scoreManager != null)
        {
            scoreManager.AddPoints(pointValue);
        }
        Instantiate(enemyexplodePrefab, transform.position, Quaternion.identity);
        //create a temporary GameObject to play the sound
        //needed to do this because destroy() was preventing
        //death sound from being made. Then, because its a new
        //audio source, I had to make a new one to control the volume
        //because the new audio source doesnt access the audio components volume control
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = shipexplodeSound;
        audioSource.volume = shipexplodesoundSource.volume; //had to add this because
        audioSource.Play();

        //destroy the temp GameObject after the sound has played
        Destroy(tempAudio, shipexplodeSound.length);
        Destroy(gameObject);

    }
    
}
