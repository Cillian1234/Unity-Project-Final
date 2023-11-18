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
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: \n" + highScore;
        }

        score = 0;
        time = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        EditHighScore();
    }

    private void EditHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: \n" + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            newHighScore.SetText("New High Score!");
        }
        score = spawnManager.fruitEaten * 50;
        scoreText.text = "Score: \n" + score;

        time += Time.deltaTime;
        timerText.text = "Time: " + Mathf.RoundToInt(time);
    }
}
