using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }
    public CanvasManager canvasManager;

    private float health, hygiene, carbs, proteins, fats, water;

    private const float MinValue = 0f;
    private const float MaxValue = 20f;
    public float Health { get => health; set => health = Mathf.Clamp(value, MinValue, MaxValue); }
    public float Hygiene { get => hygiene; set => hygiene = Mathf.Clamp(value, MinValue, MaxValue); }
    public float Carbs {  get => carbs; set => carbs = Mathf.Clamp(value, MinValue, MaxValue); }
    public float Proteins {  get => proteins; set => proteins = Mathf.Clamp(value, MinValue, MaxValue); }
    public float Fats {  get => fats; set => fats = Mathf.Clamp(value, MinValue, MaxValue); }
    public float Water {  get => water; set => water = Mathf.Clamp(value, MinValue, MaxValue); }

    //Finds instances of player stats and destroys if there are any
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            canvasManager = FindAnyObjectByType<CanvasManager>();
            LoadStats(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Updates Slider UI on screen
    public void RefreshUI()
    {
        var canvas = FindAnyObjectByType<CanvasManager>();
        if (canvas != null)
        {
            canvas.UpdateUI();
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    //Loads stats
    public void LoadStats()
    {
        Health = PlayerPrefs.GetFloat("Health", 20f);
        Hygiene = PlayerPrefs.GetFloat("Hygiene", 20f);
        Carbs = PlayerPrefs.GetFloat("Carbs", 20f);
        Proteins = PlayerPrefs.GetFloat("Proteins", 20f);
        Fats = PlayerPrefs.GetFloat("Fats", 20f);
        Water = PlayerPrefs.GetFloat("Water", 20f);
    }

    //Saves Stats
    public void SaveStats()
    {
        PlayerPrefs.SetFloat("Health", Health);
        PlayerPrefs.SetFloat("Hygiene", Hygiene);
        PlayerPrefs.SetFloat("Carbs", Carbs);
        PlayerPrefs.SetFloat("Proteins", Proteins);
        PlayerPrefs.SetFloat("Fats", Fats);
        PlayerPrefs.SetFloat("Water", Water);
        PlayerPrefs.Save();
    }
}
