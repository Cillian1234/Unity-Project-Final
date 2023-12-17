using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    private int highScore;
    private float time;

    [SerializeField]private SpawnManager spawnManager;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI highScoreText;
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI newHighScore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore")) // If the player already has a high score, get it and display it
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: \n" + highScore;
        }

        score = 0; // reset variables
        time = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        EditHighScore();
    }

    private void EditHighScore()
    {
        if (score > highScore) // update high score text and save new highscore to player preferences
        {
            highScore = score;
            highScoreText.text = "High Score: \n" + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            newHighScore.SetText("New High Score!");
        }
        score = spawnManager.fruitEaten * 50; // calculate score
        scoreText.text = "Score: \n" + score; // update text

        time += Time.deltaTime; // calculate time
        timerText.text = "Time: " + Mathf.RoundToInt(time); // update text
    }
}
