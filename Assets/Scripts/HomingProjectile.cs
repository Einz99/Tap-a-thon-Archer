using System.Collections;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform target; // Target to follow (e.g., the player)
    public float speed = 5f; // Speed of the projectile
    public float homingDuration = 20f; // Time the projectile will home in on the target
    public float destroyAfter = 20f; // Time after which the projectile is destroyed

    private Rigidbody2D rb;
    private bool isHoming = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("HomingProjectile requires a Rigidbody2D component.");
        }
        StartCoroutine(HomingTimer());
        Destroy(gameObject, destroyAfter);
    }

    void FixedUpdate()
    {
        if (isHoming && target != null)
        {
            // Calculate the direction to the target
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;

            // Apply velocity toward the target
            rb.linearVelocity = direction * speed;

            // Rotate the projectile to face the target
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            rb.rotation = angleToFace + 180;
        }
    }

    private IEnumerator HomingTimer()
    {
        // Allow homing behavior for the specified duration
        yield return new WaitForSeconds(homingDuration);
        isHoming = false; // Stop homing after duration
    }
}