using UnityEngine;

public class PlayerControllerDeleteAfter : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;     // Reference to the Rigidbody2D
    private Vector2 movement;   // Stores movement input

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal"); // Left/Right input
        movement.y = Input.GetAxisRaw("Vertical");   // Up/Down input
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }
}
