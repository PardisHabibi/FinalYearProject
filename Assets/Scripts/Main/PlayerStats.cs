using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    public float Health { get; set; }
    public float Hygiene { get; set; }
    public float Carbs {  get; set; }
    public float Proteins {  get; set; }
    public float Fats {  get; set; }
    public float Water {  get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            LoadStats(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    public void LoadStats()
    {
        Health = PlayerPrefs.GetFloat("Health", 20f);
        Hygiene = PlayerPrefs.GetFloat("Hygiene", 20f);
        Carbs = PlayerPrefs.GetFloat("Carbs", 20f);
        Proteins = PlayerPrefs.GetFloat("Proteins", 20f);
        Fats = PlayerPrefs.GetFloat("Fats", 20f);
        Water = PlayerPrefs.GetFloat("Water", 20f);
    }

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
