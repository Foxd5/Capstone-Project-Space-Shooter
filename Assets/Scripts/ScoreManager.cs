using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private int currentScore = 0;
    private int enemiesKilled = 0;

    void Start()
    {
        UpdateScoreText();
    }
  
    public void AddPoints(int points)
    {
        currentScore += points;
        enemiesKilled++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        string formattedScore = currentScore.ToString("D6");
        scoreText.text = "Score: " + formattedScore + "\nEnemies Killed: " + enemiesKilled;
    }
}
