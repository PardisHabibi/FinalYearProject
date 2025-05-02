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
    public float attackRate;
    public Bullet bullet;
    public GameObject win;
    [SerializeField] private AudioClip winSoundClip;

    //
    private void Awake()
    {
        InvaderSpawn();
        invadersAlive = TotalInvaders;
    }

    //
    public void Start()
    {
        InvokeRepeating(nameof(InvaderAttack), attackRate, attackRate);
    }

    //
    private void Update()
    {
        InvaderMovement();
        if (invadersAlive <= 0)
        {
            Win();
        }
    }

    //
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

    //
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

    //
    private void Advance()
    {
        direction.x *= -1f;
        Vector3 position = transform.position;
        position.y -= 0.3f;
        transform.position = position;
    }

    //
    private void InvaderDestroyed()
    {
        speed += 0.1f;
        invadersAlive--;
    }

    //
    private void InvaderAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / (float)invadersAlive))
            {
                //Instantiate(bullet, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    //
    private void Win()
    {
        SoundManager.instance.PlaySoundClip(winSoundClip, transform, 1f);
        Time.timeScale = 0f;
        win.SetActive(true);
    }
}
