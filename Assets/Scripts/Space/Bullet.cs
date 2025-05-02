using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    // moves bullet
    private void Update()
    {
         transform.position += direction * speed * Time.deltaTime;
    }

    // destroys gameobject the bullet collides with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        destroyed.Invoke();
    }
}
