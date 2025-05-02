using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float edge;

    // Find the edge of the camera
    private void Start()
    {
        edge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }

    // Moves obstacles and destroys once they reach the end of the screen
    private void Update()
    {
        transform.position += Vector3.left * GameManagerDino.Instance.Speed * Time.deltaTime;

        if (transform.position.x < edge)
        {
            Destroy(gameObject);
        }
    }
}
