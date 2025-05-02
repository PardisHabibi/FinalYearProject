using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodMove : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform rectTransform;
    public BoxCollider2D plateArea;

    private static List<GameObject> foodOnPlate = new List<GameObject>();
    private bool isOnPlate = false;

    public float carbs;
    public float proteins;
    public float fats;
    public float water;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();   
    }

    //Moves food onto plate when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (plateArea == null)
        {
            Debug.LogWarning("no plate area");
            return;
        }

        if (plateArea != null && !isOnPlate)
        {
            Bounds bounds = plateArea.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 targetPosition = new Vector3(x, y, 0f);
            rectTransform.position = targetPosition;
        }

        Vector2 checkPoint = new Vector2(rectTransform.position.x, rectTransform.position.y);
        if (plateArea.OverlapPoint(checkPoint))
        {
            isOnPlate = true;  // Mark as successfully on plate
            AddFood(gameObject);
            Debug.Log($"{gameObject.name} successfully added to plate.");
        }
        else
        {
            Debug.Log($"{gameObject.name} NOT inside plate area — not added.");
        }
    }

    //Adds food to a list
    private void AddFood(GameObject food)
    {
        if (!foodOnPlate.Contains(food))
        {
            foodOnPlate.Add(food);
            Debug.Log($"{food.name} added to plate.");
        }
    }

    //Eats food and updates nutrition stats
    public void Eat()
    {
        float totalCarbs = 0;
        float totalProteins = 0;
        float totalFats = 0;
        float totalWater = 0;

        foreach (GameObject food in foodOnPlate)
        {
            FoodMove foodStats = food.GetComponent<FoodMove>();
            if (foodStats != null)
            {
                totalCarbs += foodStats.carbs;
                totalProteins += foodStats.proteins;
                totalFats += foodStats.fats;
                totalWater += foodStats.water;
            }
            
        }

        PlayerStats.Instance.Carbs += totalCarbs;
        PlayerStats.Instance.Proteins += totalProteins;
        PlayerStats.Instance.Fats += totalFats;
        PlayerStats.Instance.Water += totalWater;
        PlayerStats.Instance.RefreshUI();
    }
}
