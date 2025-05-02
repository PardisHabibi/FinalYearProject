using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public Sprite healthyCleanPlayer;
    public Sprite unhealthyCleanPlayer;
    public Sprite healthyDirtyPlayer;
    public Sprite unhealthyDirtyPlayer;

    public SpriteRenderer spriteRenderer;

    public float lowHealth = 7f;
    public float lowHygiene = 7f;

    private void Update()
    {
        UpdatePlayerSprite();
    }

    // Changes sprite based on health/hygiene
    private void UpdatePlayerSprite()
    {
        float health = PlayerStats.Instance.Health;
        float hygiene = PlayerStats.Instance.Hygiene;

        if (health >= lowHealth && hygiene >= lowHygiene)
        {
            spriteRenderer.sprite = healthyCleanPlayer;
        }
        else if (health >= lowHealth && hygiene < lowHygiene)
        {
            spriteRenderer.sprite = healthyDirtyPlayer;
        }
        else if (health < lowHealth && hygiene >= lowHygiene)
        {
            spriteRenderer.sprite = unhealthyCleanPlayer;
        }
        else
        {
            spriteRenderer.sprite = unhealthyDirtyPlayer;
        }
    }
}
