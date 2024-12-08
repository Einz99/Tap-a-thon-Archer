using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBossBehavior : MonoBehaviour
{
    public Animator animator; // Animator for handling animation transitions
    public GameObject[] skillPrefabs; // Array of skill prefabs (0, 1 for basics, 2, 3 for skills, 4 for ultimate)
    public GameObject warning;
    public Transform bossPosition; // Boss's position in the center
    public Transform playerPosition; // Player's position
    public Slider BHB; // Boss HP
    private bool isAttacking = false; // Check if the boss is currently attacking
    private float animationSpeed = 1f; // Base animation speed multiplier
    private int ultcheck = 0;
    private float AttackSpeed;
    public FightCalculation FC;
    private int difficulty;
    private const string difficultyKey = "difficulty";
    public bool phase2 = false;
    private bool stayP2 = true;
    public BossHealthBar Bossbar;
    void Start()
    {
        // Set animation speed based on difficulty stored in PlayerPrefs
        difficulty = PlayerPrefs.GetInt(difficultyKey, 1); // Default to "easy" if not set
        switch (difficulty)
        {
            case 1:
                animationSpeed = .8f;
                break;
            case 2:
                animationSpeed = 1.3f;
                break;
            case 3:
                animationSpeed = 1.8f;
                break;
        }
        animator.speed = animationSpeed; // Apply the speed multiplier to the animator
        AttackSpeed = FC.attackspeed;
    }

    void Update()
    {
        if (BHB.value < 40)
        {
            ultcheck = 0;
        }
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackSequence());
        }

        // Check if phase2 is activated and handle the transformation animation
        if (phase2 && stayP2)
        {
            StartCoroutine(HandlePhase2Transformation());
            stayP2 = false;
        }
    }

    private IEnumerator HandlePhase2Transformation()
    {
        // Start the transformation animation
        animator.SetBool("Golem_Transformation", true);
        yield return new WaitForSeconds(2f); // Wait for the transformation animation duration

        // Switch to the idle animation after the transformation
        animator.SetBool("Golem_Transformation", false);
    }

    private IEnumerator AttackSequence()
    {
        // Determine attack type
        if ((BHB.value > 50f && ultcheck == 0) || (BHB.value > 75f && ultcheck == 1))
        {
            ActivateAttack(4); // Ultimate
            ultcheck++;
        }
        else
        {
            int randomValue = Random.Range(1, 101); // Random value between 1 and 100
            if (randomValue <= 70)
            {
                ActivateAttack(Random.Range(0, 2)); // Basic attack (70%)
            }
            else
            {
                ActivateAttack(Random.Range(2, 4)); // Skill attack (30%)
            }
        }

        yield return new WaitForSeconds(2f / animationSpeed); // Wait for the attack animation to finish, adjusted by speed
        isAttacking = false; // Reset for the next attack
    }

    private void ActivateAttack(int attackIndex)
    {
        if (phase2)
        {
            attackIndex += 5;
        }
        // Trigger animation
        string animationName = GetAnimationName(attackIndex);
        animator.SetBool(animationName, true);

        // Instantiate prefab at the end of the animation
        StartCoroutine(SpawnPrefabAfterAnimation(attackIndex, animationName));
    }

    private IEnumerator SpawnPrefabAfterAnimation(int attackIndex, string animationName)
    {
        yield return new WaitForSeconds(2f / animationSpeed); // Adjust delay by animation speed
        SpawnPrefab(attackIndex);

        // Reset animation to idle
        animator.SetBool(animationName, false);
    }

    private string GetAnimationName(int attackIndex)
    {
        // Returns the name of the animation parameter for each attack
        switch (attackIndex)
        {
            case 0: return "Golem_Bullet";
            case 1: return "Golem_Slam";
            case 2: return "Golem_Charge";
            case 3: return "Golem_Shield";
            case 4: return "Golem_Crystal";
            case 5: return "P2Golem_Bullet";
            case 6: return "P2Golem_Slam";
            case 7: return "P2Golem_Charge";
            case 8: return "P2Golem_Shield";
            case 9: return "P2Golem_Crystal";
            default: return "Golem_Idle";
        }
    }

    private void SpawnPrefab(int attackIndex)
    {
        if (phase2)
        {
            attackIndex -= 5;
        }
        // Ultimate will spawn multiple prefabs in a shotgun pattern
        if (attackIndex < 2)
        {
            SpawnShotgun(attackIndex);
        }
        if (attackIndex == 2)
        {
            SpawnRocksInCircle(attackIndex);
        }
        if (attackIndex == 3)
        {

        }
        else if (attackIndex == 4)
        {
            StartCoroutine(SpawnUltimateAttack(attackIndex));
        }
    }
    private void SpawnShotgun(int attackIndex)
    {
        int numProjectiles = 5; // Number of projectiles in the shotgun
        float spreadAngle = 120f; // Total spread angle
        if (phase2)
        {
            numProjectiles = 8;
            spreadAngle = 150f;
            attackIndex += 5;
        }
        float halfAngle = spreadAngle / 2f;
        for (int i = 0; i < numProjectiles; i++)
        {
            // Calculate random angle within the spread
            float angle = Random.Range(-halfAngle, halfAngle);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * (playerPosition.position - bossPosition.position).normalized;

            // Spawn prefab
            GameObject projectile = Instantiate(skillPrefabs[attackIndex], bossPosition.position, Quaternion.identity);

            // Rotate the projectile to face the direction it is moving
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate angle in degrees
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace + 180);

            // Set its velocity
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * AttackSpeed; // Adjust speed as necessary
            Destroy(projectile, 4);
        }
    }

    private void SpawnRocksInCircle(int attackIndex)
    {
        int numberOfRocks = 10;
        if (phase2)
        {
            numberOfRocks = 17;
            attackIndex += 5;
        }
        float radius = 8f;
        List<GameObject> rocks = new List<GameObject>(); // To track spawned rocks

        for (int i = 0; i < numberOfRocks; i++)
        {
            // Calculate position on the circle
            float angle = Random.Range(0f, Mathf.PI * 2);
            Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + (Vector2)bossPosition.position;

            // Instantiate rock and add to the list
            GameObject rock = Instantiate(skillPrefabs[attackIndex], spawnPosition, Quaternion.identity);
            rocks.Add(rock);
        }

        // Start the movement after spawning all rocks
        StartCoroutine(MoveRocksToTarget(rocks));
    }

    private IEnumerator MoveRocksToTarget(List<GameObject> rocks)
    {
        float duration = 5f;
        Vector2 targetPosition = new Vector2(0, -1);
        float elapsedTime = 2f;
        Dictionary<GameObject, Vector2> startPositions = new Dictionary<GameObject, Vector2>();

        // Store each rock's initial position
        foreach (GameObject rock in rocks)
        {
            startPositions[rock] = rock.transform.position;
        }

        while (elapsedTime < duration)
        {
            foreach (GameObject rock in rocks)
            {
                if (rock != null)
                {
                    // Interpolate position
                    Vector2 startPosition = startPositions[rock];
                    rock.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);

                    // Rotate the rock to face the target
                    Vector2 direction = (targetPosition - (Vector2)rock.transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rock.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure all rocks reach the target and destroy them
        foreach (GameObject rock in rocks)
        {
            if (rock != null)
            {
                rock.transform.position = targetPosition;
                Destroy(rock);
            }
        }
    }

    public void GolemShilded(int attackIndex)
    {
        float duration = 20f;
        if (phase2)
        {
            duration = 40f;
            attackIndex += 5;
        }
        GameObject shockwave = Instantiate(skillPrefabs[attackIndex], new Vector2(0f, -1f), Quaternion.identity);
        StartCoroutine(Bossbar.DefenseTimer(duration));
        Destroy(shockwave, 5f);
    }

    private IEnumerator SpawnUltimateAttack(int attackIndex)
    {
        if(phase2) attackIndex += 5;
        // Define the 8 directions for spawning warning indicators and projectiles
        Vector3[] directions = new Vector3[]
        {
        Vector3.up, // Up
        new Vector3(1f, 1f, 0).normalized, // Top-right diagonal
        Vector3.right, // Right
        new Vector3(1f, -1f, 0).normalized, // Bottom-right diagonal
        Vector3.down, // Down
        new Vector3(-1f, -1f, 0).normalized, // Bottom-left diagonal
        Vector3.left, // Left
        new Vector3(-1f, 1f, 0).normalized // Top-left diagonal
        };

        // Instantiate the warning indicators in all 8 directions
        List<GameObject> indicators = new List<GameObject>();
        foreach (var direction in directions)
        {
            // Instantiate warning at each direction, placed at a fixed distance from the boss
            Vector3 warningPosition = bossPosition.position + direction * 5f;
            GameObject indicator = Instantiate(warning, warningPosition, Quaternion.identity);
            indicators.Add(indicator);
        }

        // Wait for a brief moment to show the warning
        yield return new WaitForSeconds(0.5f);

        // Destroy the warning indicators before spawning the projectiles
        foreach (var indicator in indicators)
        {
            Destroy(indicator); // Destroy the warning after the delay
        }

        // Wait a moment before the attack starts to give the player time to react
        yield return new WaitForSeconds(0.5f);

        // Spawn projectiles in the ultimate attack pattern
        SpawnProjectilesInLines(attackIndex, directions);
    }

    private void SpawnProjectilesInLines(int attackIndex, Vector3[] directions)
    {
        int numProjectiles = 5; // Number of projectiles per direction

        // Spawn projectiles in each of the 8 directions
        foreach (var direction in directions)
        {
            StartCoroutine(SpawnMultipleProjectiles(direction, attackIndex, numProjectiles));
        }
    }

    private IEnumerator SpawnMultipleProjectiles(Vector3 direction, int attackIndex, int numProjectiles)
    {
        float delay = 0.5f; // Delay between each projectile spawn

        // Spawn 5 projectiles in the given direction
        for (int i = 0; i < numProjectiles; i++)
        {
            // Instantiate the projectile prefab at the boss's position (center)
            GameObject projectile = Instantiate(skillPrefabs[attackIndex], bossPosition.position, Quaternion.identity);

            // Set the direction of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * AttackSpeed;
            }

            // Destroy the projectile after a certain duration to avoid memory leaks
            Destroy(projectile, 3f); // Destroy after 3 seconds

            // Wait for the next projectile to spawn
            yield return new WaitForSeconds(delay);
        }
    }
}