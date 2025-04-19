using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 1f;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //start animating sprite
    private void Start()
    {
        InvokeRepeating(nameof(spriteAnimation), 0.15f, 0.15f);
    }

    //Reset position on restart
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Controls for sprite movement
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    // 'Animate' Sprite
    private void spriteAnimation()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    //Called when the bird collides with any trigger object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lose")
        {
            FindAnyObjectByType<GameManagerBird>().GameOver();
        } else if (other.gameObject.tag == "Score")
        {
            FindAnyObjectByType<GameManagerBird>().IncreaseScore();
        }
    }
}
