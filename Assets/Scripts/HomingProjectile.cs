using System.Collections;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform target; // The target to follow
    public float speed = 5f; // Speed of the projectile during homing
    public float homingDuration = 10f; // Duration for homing behavior
    public float destroyAfter = 20f; // Time after which the projectile is destroyed
    public float postHomingSpeedMultiplier = 5f; // Multiplier for speed after homing ends

    private Rigidbody2D rb;
    private bool isHoming = true; // Determines if the projectile is in homing mode
    private Vector2 lastDirection; // Stores the last direction when homing ends

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("HomingProjectile requires a Rigidbody2D component.");
        }

        StartCoroutine(HomingTimer()); // Start the homing duration timer
        Destroy(gameObject, destroyAfter); // Destroy the projectile after a set time
    }

    void FixedUpdate()
    {
        if (isHoming && target != null)
        {
            // Calculate the direction toward the target
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;

            // Move the projectile toward the target
            rb.linearVelocity = direction * speed;

            // Rotate the projectile to face the target
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.SetRotation(angleToFace + 180);

            // Store the last direction for when homing ends
            lastDirection = direction;
        }
        else if (!isHoming)
        {
            // Move in the last direction at increased speed after homing ends
            rb.linearVelocity = lastDirection * speed * postHomingSpeedMultiplier;
        }
    }

    private IEnumerator HomingTimer()
    {
        // Homing behavior lasts for the specified duration
        yield return new WaitForSeconds(homingDuration);
        isHoming = false; // Stop homing
    }
}
