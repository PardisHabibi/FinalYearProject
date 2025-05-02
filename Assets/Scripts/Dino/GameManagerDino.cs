using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDino : MonoBehaviour
{
    public static GameManagerDino Instance { get; private set; }
    public float initialSpeed = 1f;
    public float speedIncrease = 2f;
    public float Speed { get; private set; }
    [SerializeField] private Character character;
    [SerializeField] private ObstacleSpawner spawner;
    [SerializeField] private AudioClip gameOverSoundClip;

    public GameObject play;
    public GameObject gameOver;
    public GameObject retry;
    public TextMeshProUGUI instruction;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    private float score;

    //create the instance and destroy any existing ones
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            DestroyImmediate(Instance);
        }

        UpdateHighscore();
        Time.timeScale = 0f;
        Speed = 0f;
        enabled = false;
        gameOver.SetActive(false);
        retry.SetActive(false);
        play.SetActive(true);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    //increase game speed overtime
    private void Update()
    {
        Speed += speedIncrease * Time.deltaTime;
        score += Speed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    // Starts the game and spawner
    public void Play()
    {
        ObstacleMovement[] obstacles = FindObjectsOfType<ObstacleMovement>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        Speed = initialSpeed;
        Time.timeScale = 1f;
        score = 0f;
        enabled = true;

        character.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        instruction.gameObject.SetActive(false);
        play.SetActive(false);
        gameOver.SetActive(false);
        retry.SetActive(false);

        UpdateHighscore();
    }

    // reloads scene
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Ends Game and changes stats
    public void GameOver()
    {
        SoundManager.instance.PlaySoundClip(gameOverSoundClip, transform, 1f);
        Speed = 0f;
        Time.timeScale = 0f;
        enabled = false;
        UpdateHighscore();

        character.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOver.SetActive(true);
        retry.SetActive(true);

        PlayerStats.Instance.Health -= 1f;
        PlayerStats.Instance.Hygiene -= 0.5f;
        PlayerStats.Instance.Carbs -= 2f;
        PlayerStats.Instance.Proteins -= 1f;
        PlayerStats.Instance.Fats -= 0.5f;
        PlayerStats.Instance.Water -= 0f;
        PlayerStats.Instance.RefreshUI();

        Debug.Log("lose");
    }

    //Updates Highscore
    private void UpdateHighscore()
    {
        float dinoHighscore = PlayerPrefs.GetFloat("dinoHighscore", 0);
        if (score > dinoHighscore)
        {
            dinoHighscore = score;
            PlayerPrefs.SetFloat("dinoHighscore", dinoHighscore);
        }

        highscoreText.text = Mathf.FloorToInt(dinoHighscore).ToString();
    }
}
