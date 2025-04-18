using System.Security.Cryptography;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed;
    private float edge;

    // Find the edge of the camera
    private void Start()
    {
        edge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    // Moves pipes and destroys once they reach the end of the screen
    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < edge)
        {
            Destroy(gameObject);
        }
    }
}
