using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{

    public GameObject[] orbitShapes;
    private GameObject selectedShape;
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

    public MasterVolume sfx;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;

        int randomIndex = 0;
        moveSpeed = PC.moveSpeed;
        int random5percent = Random.Range (0, 101);
        if (random5percent <= 96){
            randomIndex = Random.Range(0, orbitShapes.Length-1);
        }
        else
        {
            randomIndex = 6;
        }
        selectedShape = Instantiate(
            orbitShapes[randomIndex],
            new Vector3(0, -1, 0),
            orbitShapes[randomIndex].transform.rotation // Use prefab's rotation
        );

        // Get the path points from the selected shape
        var collider = selectedShape.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            pathPoints = new List<Vector2>(collider.points);

            // Transform points to world space, considering position and rotation
            for (int i = 0; i < pathPoints.Count; i++)
            {
                Vector3 worldPoint = selectedShape.transform.TransformPoint(pathPoints[i]);
                pathPoints[i] = new Vector2(worldPoint.x, worldPoint.y);
            }
        }
        else
        {
            Debug.LogError("Selected shape must have a PolygonCollider2D.");
        }

        if (pathPoints.Count > 0)
        {
            transform.position = pathPoints[0];
        }


    }

    void Update()
    {


    if (isHolding) return;
    if (pathPoints == null || pathPoints.Count == 0) return;

    // Get the current target point
    Vector2 target = pathPoints[currentIndex];

    // Move towards the target point
    transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

    // Allow the player to change direction at any time
    if (Input.GetKeyDown(KeyCode.Space))  // Change this to your preferred input key
    {
        ChangeDirection();
    }

    // Check if the archer has reached the current target
    if (Vector2.Distance(transform.position, target) < 0.01f)
    {
        // Move to the next point in the current direction
        currentIndex = (currentIndex + direction + pathPoints.Count) % pathPoints.Count;
    }

    // Update archer's rotation towards the boss
    if (boss == null)
    {
        return;
    }

    Vector3 bossDirection = (boss.position - transform.position).normalized;

    if (bossDirection != Vector3.zero)
    {
        float angle = Mathf.Atan2(bossDirection.y, bossDirection.x) * Mathf.Rad2Deg;

        angle -= 90f; // Adjust to match your desired rotation

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
}

public void ChangeDirection()
{
    sfx.play_SFX(sfx.TAP);
    sfx.play_SFX(sfx.SWOOSH);

    // Flip the direction immediately when the button is pressed
    currentDirect = !currentDirect;

    // Change the movement direction
    if (currentDirect)
    {
        direction = 1;  // Move towards the next point in the path
    }
    else
    {
        direction = -1; // Move towards the previous point in the path
    }

    // To immediately change the target direction:
    // Calculate the new target based on the current direction.
    // Move to the next or previous point, depending on direction.
    currentIndex = (currentIndex + direction + pathPoints.Count) % pathPoints.Count;
}
    void OnTriggerEnter2D(Collider2D x)
    {
        if (isCooldown) return; // Skip if on cooldown

        if (x.gameObject.CompareTag("Boss Attack"))
        {
            minusHeart();
            StartCoroutine(CooldownRoutine());
        }

        if (x.gameObject.CompareTag("Obstacle"))
        {

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
        flash();
        

        if (heartContainer.transform.childCount != 0)
        {
            sfx.play_SFX(sfx.PLAYER_DAMAGED);
            Transform child = heartContainer.transform.GetChild(0);
            Destroy(child.gameObject);
            if (PlayerPrefs.GetInt(vibrateKey, 0) == 0)
            {
                //Handheld.Vibrate();
            }
        }
        if (heartContainer.transform.childCount == 1)
                {
                    Time.timeScale = 0;
                    pause.SetActive(false);
                    LosingPage.SetActive(true);
                    LosingPage.GetComponent<UIAnimation>().Open();
                    LosingPage.GetComponent<PlayUISprite>().playLoopedAnimaion();
                }
    }

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    public SpriteRenderer spriteRenderer;

    public Material originalMaterial;

    private Coroutine flashRoutine;

    public void flash(){
        if(flashRoutine != null){
            StopCoroutine(flashRoutine);
        }


        flashRoutine = StartCoroutine(flashCoroutine());
    }

    private IEnumerator flashCoroutine(){

        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }
}
