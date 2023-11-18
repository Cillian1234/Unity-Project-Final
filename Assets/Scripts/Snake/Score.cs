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
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: \n" + highScore;
        }

        score = 0;
        time = 0;
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        editHighScore();
        if (spawnManager.isGameOver == false)
        {
            score = spawnManager.fruitEaten * 50;
            scoreText.text = "Score: " + score;

            time += Time.deltaTime;
            timerText.text = "Time: " + Mathf.RoundToInt(time);
        }
    }

    private void editHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
