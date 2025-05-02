using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 3;
    public int cols = 14;

    public float speed = 0.5f;
    private Vector3 direction = Vector2.right;

    public int TotalInvaders => rows * cols;
    public int invadersAlive;
    public GameObject win;

    //Calls to spawn invaders
    private void Awake()
    {
        InvaderSpawn();
        invadersAlive = TotalInvaders;
    }

    //Wins game when there are no invaders left
    private void Update()
    {
        InvaderMovement();
        if (invadersAlive <= 0)
        {
            Win();
        }
    }

    //Spawns invaders in a grid pattern
    private void InvaderSpawn()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = 1f * (cols - 1);
            float height = 1f * (rows - 1);

            Vector2 center = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(center.x, (1f * row) + center.y, 0f);

            for (int col = 0; col < cols; col++)
            {
                Invader invader = Instantiate(prefabs[row], transform);
                invader.InvaderHit += InvaderDestroyed;
                Vector3 position = rowPosition;
                position.x += col * 1f;
                invader.transform.localPosition = position;
            }
        }
    }

    //moves invaders left to right and down
    private void InvaderMovement()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        
        transform.position += direction * speed * Time.deltaTime;
        
        foreach(Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.2f))
            {
                Advance();
            } else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.2f))
            {
                Advance();
            }
        }
    }

    //moves invaders down
    private void Advance()
    {
        direction.x *= -1f;
        Vector3 position = transform.position;
        position.y -= 0.3f;
        transform.position = position;
    }

    // makes invaders faster as more die
    private void InvaderDestroyed()
    {
        speed += 0.1f;
        invadersAlive--;
    }

    // wins game
    private void Win()
    {
        Time.timeScale = 0f;
        win.SetActive(true);
    }
}
