using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount; // #enemies to spawn in this wave
        //public float spawnDelay; // delay between spawns in this wave (unused)
    }

    public Wave[] waves; 
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints; 

    private int currentWaveIndex = 0;
    //private int enemiesSpawnedInCurrentWave = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnWave()
    {
        if (currentWaveIndex >= waves.Length) return; // stop if all waves are complete

        int enemiesToSpawn = waves[currentWaveIndex].enemyCount;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;

        // Subscribe to the enemy's death event
        EnemyHealthManager enemyHealth = newEnemy.GetComponent<EnemyHealthManager>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    void HandleEnemyDeath()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            // Move to the next wave
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
            {
                SpawnWave(); // Spawn the next wave
            }
            else
            {
                // Optionally, trigger level completion logic here
            }
        }
    }

    void LevelCompleted()
    {
        //Debug.Log("Level Completed!");
        // Trigger level completion logic here
    }
}
