using UnityEngine;
using UnityEngine.SceneManagement;

public class SpacePlayer : MonoBehaviour
{
    public Bullet bullet;
    public float speed = 5f;

    private bool bulletActive;

    public GameObject instructions;
    public GameObject play;
    public GameObject gameOver;
    public GameObject retry;

    [SerializeField] private AudioClip gameOverSoundClip;

    private void Awake()
    {
        Time.timeScale = 0f;
        play.SetActive(true);
    }

    //Starts Game
    public void Play()
    {
        Time.timeScale = 1f;
        instructions.SetActive(false);
        play.SetActive(false);
    }

    //Moves player and shoots on keypress
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } 
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    //Spawns bullet and detroys when necessary
    private void Shoot()
    {
        if (!bulletActive)
        {
            Bullet projectile = Instantiate(bullet, transform.position, Quaternion.identity);
            projectile.destroyed += BulletHit;
            bulletActive = true;
        }
    }

    private void BulletHit()
    {
        bulletActive = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ends game and decreases stats when invader and player collide
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("Lose"))
        {
            SoundManager.instance.PlaySoundClip(gameOverSoundClip, transform, 1f);
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            retry.SetActive(true);
            PlayerStats.Instance.Health--;
            PlayerStats.Instance.Hygiene -= 0.5f;
            PlayerStats.Instance.Carbs -= 1f;
            PlayerStats.Instance.Proteins -= 1f;
            PlayerStats.Instance.Fats -= 1f;
            PlayerStats.Instance.Water -= 1f;
            PlayerStats.Instance.RefreshUI();
        }
    }
}
