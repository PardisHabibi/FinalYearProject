using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreText;
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
        Time.timeScale = 0f;
        Debug.Log("lose");
    }

    //Increase when player gets through pipe
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
