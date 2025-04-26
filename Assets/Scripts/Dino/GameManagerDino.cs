using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerDino : MonoBehaviour
{
    public static GameManagerDino Instance { get; private set; }
    public float initialSpeed = 1f;
    public float speedIncrease = 2f;
    public float speed { get; private set; }
    private Character character;
    private ObstacleSpawner spawner;

    public GameObject play;
    public GameObject gameOver;
    public GameObject retry;

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

        speed = 0f;
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

    //set starting game speed
    private void Start()
    {
        character = FindAnyObjectByType<Character>();
        spawner = FindAnyObjectByType<ObstacleSpawner>();

        Play();
    }

    //increase game speed overtime
    private void Update()
    {
        speed += speedIncrease * Time.deltaTime;
        score += speed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void Play()
    {
        ObstacleMovement[] obstacles = FindObjectsOfType<ObstacleMovement>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        speed = initialSpeed;
        score = 0f;
        enabled = true;

        character.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        play.SetActive(false);
        gameOver.SetActive(false);
        retry.SetActive(false);

        UpdateHighscore();
    }

    public void GameOver()
    {
        speed = 0f;
        enabled = false;
        UpdateHighscore();

        PlayerStats.Instance.Health--;
        PlayerStats.Instance.Hygiene -= 0.5f;
        PlayerStats.Instance.Carbs -= 0f;
        PlayerStats.Instance.Proteins -= 0f;
        PlayerStats.Instance.Fats -= 0f;
        PlayerStats.Instance.Water -= 0f;

        character.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOver.SetActive(true);
        retry.SetActive(true);
        Debug.Log("lose");
    }

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
