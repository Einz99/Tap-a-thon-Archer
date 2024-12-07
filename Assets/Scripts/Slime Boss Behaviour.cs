using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBossBehavior : MonoBehaviour
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
            case 0: return "Slime_Bullet";
            case 1: return "Slime_Splash";
            case 2: return "Slime_Crystal";
            case 3: return "Golem_Shield";
            case 4: return "Golem_Slam";
            default: return "Slime_Idle";
        }
    }

    private void SpawnPrefab(int attackIndex)
    {
        // Ultimate will spawn multiple prefabs in a shotgun pattern
        if (attackIndex < 2)
        {
            SpawnShotgun(attackIndex);
        }
        if (attackIndex == 2)
        {
            StartCoroutine(SpawnStompLine(attackIndex));
        }
        if (attackIndex == 3)
        {
            SpawnHomingProjectile(attackIndex);
        }
        else if (attackIndex == 4)
        {
            StartCoroutine(SpawnUltimateAttack());
        }
    }

    private void SpawnShotgun(int attackIndex)
    {
        int numProjectiles = 5; // Number of projectiles in the shotgun
        float spreadAngle = 120f; // Total spread angle
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

    private void SpawnHomingProjectile(int attackIndex)
    {
        float speedLesser = 0;
        if (difficulty == 1) speedLesser = 0.5f;
        if (difficulty == 2) speedLesser = 1.5f;
        if (difficulty == 3) speedLesser = 2.5f;
        // Spawn the projectile directly in front of the boss
        GameObject projectile = Instantiate(skillPrefabs[attackIndex], bossPosition.position, Quaternion.identity);

        // Get the HomingProjectile component and configure its target
        HomingProjectile homingProjectile = projectile.AddComponent<HomingProjectile>();
        homingProjectile.target = playerPosition;
        homingProjectile.speed = AttackSpeed - speedLesser; // Adjust speed as necessary
        homingProjectile.homingDuration = 3f; // Time for the projectile to follow the target
        homingProjectile.destroyAfter = 30f; // Time before the projectile self-destructs
    }

    private IEnumerator SpawnStompLine(int attackIndex)
    {
        int numPrefabs = 8; // Number of prefabs to spawn
        float distanceBetween = 1f; // Distance between each prefab
        float spawnDelay = 0.5f; // Delay between each spawn (1 or 1.5 seconds)
        for (int j = 0; j < 2; j++)
        {
            Vector3 direction = (playerPosition.position - bossPosition.position).normalized;
            for (int i = 0; i < numPrefabs; i++)
            {
                // Calculate spawn position along the line toward the player
                Vector3 spawnPosition = bossPosition.position + direction * distanceBetween * (i + 1);

                // Instantiate the prefab at the calculated position
                GameObject stompPrefab = Instantiate(skillPrefabs[attackIndex], spawnPosition, Quaternion.identity);

                // Optionally, orient the prefab to face the player
                float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                stompPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace);

                // Destroy the prefab after 3 seconds
                Destroy(stompPrefab, 3f);

                // Wait for the specified delay before spawning the next prefab
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    private IEnumerator SpawnUltimateAttack()
    {
        // Step 1: Spawn the warning prefab around the boss
        float warningDuration = 2f; // Duration the warning stays visible
        float warningRadius = 4f; // Radius around the boss where warnings will spawn
        List<GameObject> warningPrefabs = new List<GameObject>(); // Store the warning prefab instances
        List<Vector3> warningPositions = new List<Vector3>(); // Store the positions for ultimate spawns

        // Randomly spawn multiple warning prefabs around the boss and store their positions
        for (int i = 0; i < 10; i++) // 10 warning prefabs, adjust as needed
        {
            Vector3 randomPosition = bossPosition.position +
                                     new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0f) * warningRadius;

            GameObject warningPrefab = Instantiate(warning, randomPosition, Quaternion.identity);
            warningPrefabs.Add(warningPrefab); // Store the prefab instance
            warningPositions.Add(randomPosition); // Store the spawn position for later
        }

        // Wait for the warning duration
        yield return new WaitForSeconds(warningDuration);

        // Step 2: Destroy all warning prefabs after the warning duration
        foreach (GameObject warningPrefab in warningPrefabs)
        {
            Destroy(warningPrefab); // Destroy the warning prefab
        }

        // Step 3: Spawn the ultimate skill prefabs at the same positions as the warnings
        foreach (Vector3 position in warningPositions)
        {
            GameObject ultimatePrefab = Instantiate(skillPrefabs[4], position, Quaternion.identity);

            // Optionally, orient the prefab to face the player or any other direction
            Vector3 direction = (playerPosition.position - position).normalized;
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ultimatePrefab.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace);

            // Destroy the ultimate prefab after a few seconds
            Destroy(ultimatePrefab, 2f); // Destroy the ultimate prefab after 2 seconds (adjust as needed)
        }

        // Wait for the ultimate prefabs to finish their effect and destroy
        yield return new WaitForSeconds(3f); // Wait for the ultimate prefabs to be destroyed
    }
}