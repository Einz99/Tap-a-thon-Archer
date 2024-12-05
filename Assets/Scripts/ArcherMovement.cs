using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{

    public GameObject outlineShape;
    private List<Vector2> pathPoints;
    private int currentIndex = 0;
    public float moveSpeed = 2f;
    private int direction = 1;
    public bool currentDirect = true;
    public Transform boss;
    public bool isHolding = false;
    public PlayerCalculation PC;
    public GameObject heartContainer;
    public const string vibrateKey = "vibrateMode";
    private bool isCooldown = false; // Flag to track cooldown
    public GameObject pause;
    public GameObject LosingPage;
    void Start()
    {
        moveSpeed = PC.moveSpeed;
        var collider = outlineShape.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            pathPoints = new List<Vector2>(collider.points);
            for (int i = 0; i < pathPoints.Count; i++)
            {
                pathPoints[i] = outlineShape.transform.TransformPoint(pathPoints[i]);
            }
        }
        else
        {
            Debug.LogError("OutlineShape must have a PolygonCollider2D.");
        }

        if (pathPoints.Count > 0)
        {
            transform.position = pathPoints[0];
        }
    }

    void Update()
    {
        
        if (heartContainer.transform.childCount == 0)
        {
            Time.timeScale = 0;
            pause.SetActive(false);
            LosingPage.SetActive(true);
        }
        if (isHolding) return;
        if (pathPoints == null || pathPoints.Count == 0) return;

        Vector2 target = pathPoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            currentIndex = (currentIndex + direction + pathPoints.Count) % pathPoints.Count;
        }

        if (boss == null)
        {
            return;
        };

        Vector3 bossdirection = (boss.position - transform.position).normalized;

        if (bossdirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(bossdirection.y, bossdirection.x) * Mathf.Rad2Deg;

            angle -= 90f;

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public void ChangeDirection()
    {
        currentDirect = !currentDirect;
        if (currentDirect)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D x)
    {
        if (isCooldown) return; // Skip if on cooldown

        if (x.gameObject.CompareTag("Boss Attack"))
        {
            minusHeart();
            StartCoroutine(CooldownRoutine());
        }

        if(x.gameObject.CompareTag("Obstacle")){
            ChangeDirection();
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldown = true; // Activate cooldown
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        isCooldown = false; // Reset cooldown
    }

    private void minusHeart()
    {
        if (heartContainer.transform.childCount != 0)
        {
            Transform child = heartContainer.transform.GetChild(0);
            Destroy(child.gameObject);
            if (PlayerPrefs.GetInt(vibrateKey, 0) == 0)
            {
                Handheld.Vibrate();
            }
        }
    }
}
