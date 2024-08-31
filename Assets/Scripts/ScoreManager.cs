using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private TextMeshProUGUI scoreText;
    private int currentScore = 0;
    private int enemiesKilled = 0;

    //need to record initial scoreboard info incase a level is restarted
    private int initialScore;
    private int initialEnemiesKilled;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
           // Debug.Log("ScoreManager initialized and set");
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log("Duplicate destroyed");
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        initialScore = currentScore;
        initialEnemiesKilled = enemiesKilled;
        FindScoreTextInScene();
        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        enemiesKilled++;
        UpdateScoreText();
    }

    public void ResetScoreToInitial()
    {
        //reset the score and kills to the initial values
        currentScore = initialScore;
        enemiesKilled = initialEnemiesKilled;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            string formattedScore = currentScore.ToString("D6");
            scoreText.text = "Score: " + formattedScore + "\nEnemies Killed: " + enemiesKilled;
        }
       
    }
    //need to find the textmesh object so I can be consistent across scenes
    private void FindScoreTextInScene()
    {
        GameObject scoreTextObject = GameObject.Find("Scoreboard");

        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            //Debug.Log("scoreText found and assigned");
        } 
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
}

