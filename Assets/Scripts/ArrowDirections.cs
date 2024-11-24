using UnityEngine;

public class ArrowDirections : MonoBehaviour
{
    public Transform target; // Target to move towards
    public float speed = 5f; // Speed of movement

    private void Update()
    {
        if (target == null) return;

        // Calculate direction
        Vector3 direction = (target.position - transform.position).normalized;

        // Move towards the target
        transform.position += direction * speed * Time.deltaTime;

        // Rotate based on the direction with an additional 90-degree offset
        if (direction != Vector3.zero)
        {
            // Get the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply a 90-degree rotation offset around the desired axis
            targetRotation *= Quaternion.Euler(0f, 90f, 90f); // Modify the axis as needed

            // Smoothly rotate towards the target with the offset
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

   void OnTriggerEnter2D(Collider2D x)
   {
        if(x.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
   }
}
