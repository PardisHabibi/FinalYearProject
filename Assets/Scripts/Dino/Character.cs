using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public float gravity = 9.8f * 3;
    public float force = 10f;

    //Get character controller first
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    //Jumping and resetting gravity
    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded) 
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * force;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    // Call to end the game if the player hits an obstacle
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lose"))
        {
            GameManagerDino.Instance.GameOver();
        }
    }
}
