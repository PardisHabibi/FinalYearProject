using UnityEngine;

public class SpacePlayer : MonoBehaviour
{
    public Bullet bullet;
    public float speed = 5f;

    private bool bulletActive;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("InvaderBullet"))
        {
            // LOSE
        }
    }
}
