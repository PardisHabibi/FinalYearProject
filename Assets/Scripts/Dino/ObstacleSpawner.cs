using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject spawnObject;
        [Range(0f, 1f)]
        public float chance;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    //call to spawn
    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    //cancel spawn
    private void OnDisable()
    {
        CancelInvoke();
    }

    //spawn obstacles based on chance
    private void Spawn()
    {
        float chance = Random.value;

        foreach (var obj in objects)
        {
            if (chance < obj.chance)
            {
                GameObject obstacle = Instantiate(obj.spawnObject);
                obstacle.transform.position += transform.position;
                break;
            }

            chance -= obj.chance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
