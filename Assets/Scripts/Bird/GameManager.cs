using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBird : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject play;
    public GameObject gameOver;
    private int score;
 
    // Start the game paused
    private void Awake()
    {
        Time.timeScale = 0f;
        play.SetActive(true);
        player.enabled = false;
    }

    //Remove extra objects and start game
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        UpdateHighscore();
        play.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();

        for (int i=0; i<pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    // End the game
    public void GameOver()
    {
        gameOver.SetActive(true);
        play.SetActive(true);
        UpdateHighscore();
        Time.timeScale = 0f;
        Debug.Log("lose");
    }

    //Increase when player gets through pipe
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    // sets users highscore
    private void UpdateHighscore()
    {
        float birdHighscore = PlayerPrefs.GetFloat("birdHighscore", 0);
        if (score > birdHighscore)
        {
            birdHighscore = score;
            PlayerPrefs.SetFloat("birdHighscore", birdHighscore);
        }

        highscoreText.text = Mathf.FloorToInt(birdHighscore).ToString();
    }
}
