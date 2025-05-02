using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakePlayer : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    public float moveInterval = 0.04f;
    private float moveTimer;
    public float moveDistance = 0.4f;

    private List<Transform> snakeBody;
    public Transform BodyPrefab;
    private readonly List<Vector3> previousPositions = new();
    public int initialSize = 3;

    public GameObject play;
    public GameObject gameOver;
    public GameObject retry;
    [SerializeField] private AudioClip gameOverSoundClip;
    [SerializeField] private AudioClip growSoundClip;


    private void Awake()
    {
        Time.timeScale = 0f;
        play.SetActive(true);
    }

    //Starts game
    public void Play()
    {
        play.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        snakeBody = new List<Transform>() { transform };
        for (int i = 0; i < initialSize; i++)
        {
            Grow();
        }
    }

    //Moves Snake in intervals
    private void Update()
    {
        MovementInput();

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            MoveSnake();
        }
    }

    //Moves snake based on users keypress
    private void MovementInput()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector3.down)
        {
            direction = Vector3.up;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector3.right)
        {
            direction = Vector3.left;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector3.up)
        {
            direction = Vector3.down;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector3.left)
        {
            direction = Vector3.right;
        }
    }

    // Moves snake
    private void MoveSnake()
    {
        Vector3 prevPosition = transform.position;
        transform.position += direction * moveDistance;

        previousPositions.Insert(0, prevPosition);

        if (previousPositions.Count >= snakeBody.Count)
        {
            for (int i = 0; i < snakeBody.Count - 1; i++)
            {
                snakeBody[i + 1].position = previousPositions[i];
            }
        }
    }

    // Adds to snake body
    private void Grow()
    {
        SoundManager.instance.PlaySoundClip(growSoundClip, transform, 1f);
        Transform body = Instantiate(BodyPrefab);
        Vector3 spawnPosition = snakeBody[^1].position;

        spawnPosition -= direction * moveDistance;
        body.position = spawnPosition;

        snakeBody.Add(body);
    }

    //Ends game if snake hits a wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            Grow();
        } else if (collision.CompareTag("Lose"))
        {
            SoundManager.instance.PlaySoundClip(gameOverSoundClip, transform, 1f);
            gameOver.SetActive(true);
            retry.SetActive(true);
            Time.timeScale = 0f;

            PlayerStats.Instance.Health -= 1f;
            PlayerStats.Instance.Hygiene -= 0.5f;
            PlayerStats.Instance.Carbs -= 1f;
            PlayerStats.Instance.Proteins -= 0.5f;
            PlayerStats.Instance.Fats -= 2f;
            PlayerStats.Instance.Water -= 2f;
            PlayerStats.Instance.RefreshUI();
            Debug.Log("lose");
        }
    }

    //reloads scene
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
