using TMPro;
using UnityEngine;


public class GameManagerBird : MonoBehaviour
{
    public BirdPlayer player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject play;
    public GameObject gameOver;
    private int score;
    public TextMeshProUGUI instruction;
    [SerializeField] private AudioClip gameOverSoundClip;
    [SerializeField] private AudioClip pointSoundClip;

    // Start the game paused
    private void Awake()
    {
        Time.timeScale = 0f;
        play.SetActive(true);
        player.enabled = false;
        UpdateHighscore();
    }

    //Remove extra objects and start game
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        UpdateHighscore();
        play.SetActive(false);
        gameOver.SetActive(false);
        instruction.gameObject.SetActive(false);
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
        SoundManager.instance.PlaySoundClip(gameOverSoundClip, transform, 1f);
        gameOver.SetActive(true);
        play.SetActive(true);
        player.enabled=false;
        UpdateHighscore();

        PlayerStats.Instance.Health -= 3f;
        PlayerStats.Instance.Hygiene -= 1f;
        PlayerStats.Instance.Carbs -= 3f;
        PlayerStats.Instance.Proteins -= 2f;
        PlayerStats.Instance.Fats -= 1f;
        PlayerStats.Instance.Water -= 1f;
        PlayerStats.Instance.canvasManager.UpdateUI();
        PlayerStats.Instance.RefreshUI();

        Time.timeScale = 0f;
        Debug.Log("lose");
    }

    //Increase when player gets through pipe
    public void IncreaseScore()
    {
        SoundManager.instance.PlaySoundClip(pointSoundClip, transform, 1f);
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
