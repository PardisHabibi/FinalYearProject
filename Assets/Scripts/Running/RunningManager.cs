using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunningManager : MonoBehaviour
{
    public static RunningManager Instance { get; private set; }
    public float speed = 20f;
    public float maxSpeed = 70f;
    public float minSpeed = 20f;
    public float speedDecreaseRate = 1f;

    public GameObject play;
    public TextMeshProUGUI complete;

    public float progress;
    public float maxProgress = 500f;
    public Slider runTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        play.SetActive(false);
    }

    private void Update()
    {
        IncreaseSpeed();
        LoseSpeed();
        UpdateStats();

        progress += speed * Time.deltaTime;

        if (runTime != null)
        {
            runTime.value = progress;
        }

        if (progress >= maxProgress)
        {
            Time.timeScale = 0f;
            complete.gameObject.SetActive(true);
        }
    }
    private void IncreaseSpeed()
    {
        if (speed >= minSpeed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed += 2f;
            }
        }
        else if (speed >= 30)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed += 1f;
            }
        }
        else if (speed >= 50)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed += 0.3f;
            }
        }
        else if (speed >= maxSpeed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed += 0f;
            }
        }
    }

    private void LoseSpeed()
    {
        if (speed > minSpeed)
        {
            speed -= speedDecreaseRate * Time.deltaTime;  
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);  
        }
    }

    private void UpdateStats()
    {
        if (PlayerStats.Instance == null) { return; }
        PlayerStats.Instance.Health += speed * 0.02f * Time.deltaTime;
        PlayerStats.Instance.Hygiene -= speed * 0.01f * Time.deltaTime;
        PlayerStats.Instance.RefreshUI();
    }
}
        
