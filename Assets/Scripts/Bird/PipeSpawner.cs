using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipes;
    public float spawnRate;
    public float minimumHeight = -1f;
    public float maximumHeight = 1f;

    //call to spawn pipes
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    //Stop spawning pipes
    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
    
    //Instantiates pipes at different heights
    private void Spawn()
    {
        GameObject pipe = Instantiate(pipes, transform.position, Quaternion.identity);
        pipe.transform.position += Vector3.up * Random.Range(minimumHeight, maximumHeight);
    }
}
