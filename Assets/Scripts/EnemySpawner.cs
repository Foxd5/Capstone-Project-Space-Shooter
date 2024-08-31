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

    public GameObject endlevelPanel;
    public Wave[] waves; 
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints;

    private HealthManager healthManager;
    private int currentWaveIndex = 0;
    //private int enemiesSpawnedInCurrentWave = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        healthManager = GameObject.Find("PlayerShip").GetComponent<HealthManager>();
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
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
            {
                SpawnWave(); 
            }
            else
            {
                LevelCompleted();
            }
        }
    }

    void LevelCompleted()
    {
        //Time.timeScale = 0f; //exluding this for now because its fun to move around on level finish
        endlevelPanel.SetActive(true);

        // Trigger level completion logic here
    }
}
