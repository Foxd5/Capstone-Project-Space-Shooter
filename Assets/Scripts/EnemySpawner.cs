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

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;

    private static int totalEnemiesAlive = 0;
    private static int spawnersCompleted = 0;

    void Start()
    {
        totalEnemiesAlive = 0;//have to reset these at the start of each level
        spawnersCompleted = 0;//so you can correctly trigger level completion
        SpawnWave();
    }

    void SpawnWave()
    {

        {
            if (currentWaveIndex >= waves.Length)
            {
                spawnersCompleted++;
                CheckLevelCompletion(); // Check if all spawners have completed their waves
                return; // Stop if all waves are complete
            }

            int enemiesToSpawn = waves[currentWaveIndex].enemyCount;
            enemiesAlive += enemiesToSpawn;
            totalEnemiesAlive += enemiesToSpawn; // Track total enemies across all spawners

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
        /*if (currentWaveIndex >= waves.Length) return; // stop if all waves are complete

        int enemiesToSpawn = waves[currentWaveIndex].enemyCount;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }*/
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyHealthManager enemyHealth = newEnemy.GetComponent<EnemyHealthManager>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDeath += HandleEnemyDeath;
        }
    }


    void HandleEnemyDeath()
    {
        enemiesAlive--;
        totalEnemiesAlive--;

        if (enemiesAlive <= 0)
        {
            currentWaveIndex++;
            SpawnWave(); // Trigger next wave if current wave is done
        }

        CheckLevelCompletion(); // Check if the level should be completed
    }

    void CheckLevelCompletion()
    {
        if (totalEnemiesAlive <= 0 && spawnersCompleted == FindObjectsOfType<EnemySpawner>().Length)
        {
            LevelCompleted();
        }
    }
    void LevelCompleted()
    {
        //Time.timeScale = 0f; //exluding this for now because its fun to move around on level finish
        endlevelPanel.SetActive(true);

        // Trigger level completion logic here
    }
}
