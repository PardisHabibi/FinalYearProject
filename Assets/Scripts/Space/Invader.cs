using UnityEngine;

public class Invader : MonoBehaviour
{
    public System.Action InvaderHit;

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            InvaderHit.Invoke();
           gameObject.SetActive(false);
        }
    }
}
