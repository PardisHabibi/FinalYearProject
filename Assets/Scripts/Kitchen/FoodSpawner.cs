using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foods;
    public int foodsToSpawn = 5;
    public BoxCollider2D foodArea;
    public RectTransform plate;
    public Canvas canvas;

    private void Start()
    {
        spawnFood();
    }

    //Spawns food at random locations in set area
    private void spawnFood()
    {
        for (int i=0; i< foodsToSpawn; i++)
        {
            Bounds bounds = foodArea.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Vector3 position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);

            GameObject randomFood = foods[Random.Range(0, foods.Length)];
            Instantiate(randomFood, position, Quaternion.identity, canvas.transform);

            FoodMove foodScript = randomFood.GetComponent<FoodMove>();
            if (foodScript != null)
            {
                foodScript.plateArea = plate.GetComponent<BoxCollider2D>();
            }
        }
    }
}
