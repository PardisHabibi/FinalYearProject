using UnityEngine;

public class Invader : MonoBehaviour
{
    public System.Action InvaderHit;
    [SerializeField] private AudioClip hitSoundClip;

    // destroys invaders on collision with bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            SoundManager.instance.PlaySoundClip(hitSoundClip, transform, 1f);
            InvaderHit.Invoke();
            gameObject.SetActive(false);
        }
    }
}
