using UnityEngine;

public class ArrowDirections : MonoBehaviour
{
    public Transform target; 
    public float speed = 5f; 

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            targetRotation *= Quaternion.Euler(0f, 90f, 90f);

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
