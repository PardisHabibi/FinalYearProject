using UnityEngine;

public class DinoSpriteAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Call to start animating dino
    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    // 'Animate' dino
    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManagerDino.Instance.Speed);
    }
}
