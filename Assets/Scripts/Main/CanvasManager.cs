using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Slider health;
    public Slider hygiene;
    public Slider carbs;
    public Slider proteins;
    public Slider fats;
    public Slider water;

    private const float MaxStatValue = 20f;
    private const float MinStatValue = 0f;

    private void Start()
    {
        health.onValueChanged.AddListener(OnHealthChange);
        hygiene.onValueChanged.AddListener(OnHygieneChange);
        carbs.onValueChanged.AddListener(OnCarbsChange);
        proteins.onValueChanged.AddListener(OnProteinsChange);
        fats.onValueChanged.AddListener(OnFatsChange);
        water.onValueChanged.AddListener(OnWaterChange);

        health.value = PlayerStats.Instance.Health;
        hygiene.value = PlayerStats.Instance.Hygiene;
        carbs.value = PlayerStats.Instance.Carbs;
        proteins.value = PlayerStats.Instance.Proteins;
        fats.value = PlayerStats.Instance.Fats;
        water.value = PlayerStats.Instance.Water;
    }

    private void OnHealthChange(float value)
    {
        PlayerStats.Instance.Health = Mathf.Clamp(value, MinStatValue, MaxStatValue);
        PlayerStats.Instance.SaveStats();
    }

    private void OnHygieneChange(float value)
    {
        PlayerStats.Instance.Hygiene = Mathf.Clamp(value, MinStatValue, MaxStatValue);
        PlayerStats.Instance.SaveStats();
    }

    private void OnCarbsChange(float value)
    {
        PlayerStats.Instance.Carbs = Mathf.Clamp(value, MinStatValue, MaxStatValue); 
        PlayerStats.Instance.SaveStats();
    }

    private void OnProteinsChange(float value)
    {
        PlayerStats.Instance.Proteins = Mathf.Clamp(value, MinStatValue, MaxStatValue);
        PlayerStats.Instance.SaveStats();
    }

    private void OnFatsChange(float value)
    {
        PlayerStats.Instance.Fats = Mathf.Clamp(value, MinStatValue, MaxStatValue); 
        PlayerStats.Instance.SaveStats();
    }

    private void OnWaterChange(float value)
    {
        PlayerStats.Instance.Water = Mathf.Clamp(value, MinStatValue, MaxStatValue);
        PlayerStats.Instance.SaveStats();
    }
}
