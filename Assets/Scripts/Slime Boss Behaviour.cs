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
    public bool phase2 = false;
    private bool stayP2 = true;

    public MasterVolume sfx;

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
        animator.SetBool("Slimey_Transform", true);
        yield return new WaitForSeconds(2f); // Wait for the transformation animation duration

        // Switch to the idle animation after the transformation
        animator.SetBool("Slimey_Transform", false);
    }

    private IEnumerator AttackSequence()
    {
        // Determine attack type
        if ((BHB.value > 50f && ultcheck == 0) || (BHB.value > 75f && ultcheck == 1))
        {
            ActivateAttack(4); // Ultimate
            ultcheck += 1;
            Debug.Log("test");
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
            case 0: sfx.play_SFX(sfx.SLIME_WHIP); return "Slimey_Tentacles";
            case 1: sfx.play_SFX(sfx.SLIME_SHOOT); return "Slimey_Barrage";
            case 2: sfx.play_SFX(sfx.SLIME_SPLASH); return "Slimey_Trapper";
            case 3: sfx.play_SFX(sfx.SLIME_BLOW); return "Slimey_Bubble";
            case 4: sfx.play_SFX(sfx.SLIME_RIPPLE); return "Slimey_Ripples";
            case 5: sfx.play_SFX(sfx.SLIME_WHIP); return "P2Slimey_Tentacles";
            case 6: sfx.play_SFX(sfx.SLIME_SHOOT); return "P2Slimey_Barrage";
            case 7: sfx.play_SFX(sfx.SLIME_SPLASH); return "P2Slimey_Trapper";
            case 8: sfx.play_SFX(sfx.SLIME_BLOW); return "P2Slimey_Bubble";
            case 9: sfx.play_SFX(sfx.SLIME_RIPPLE); return "P2Slimey_Ripples";
            default: return "Bear";
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
            StartCoroutine(SpawnStompLine(attackIndex));
        }
        if (attackIndex == 3)
        {
            SpawnHomingProjectile(attackIndex);
        }
        else if (attackIndex == 4)
        {
            StartCoroutine(SpawnUltimateAttack(attackIndex));
        }
    }

    private void SpawnShotgun(int attackIndex)
    {
        sfx.play_SFX(sfx.SLIME_BULLET);
        
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

    private void SpawnHomingProjectile(int attackIndex)
    {;
        sfx.play_SFX(sfx.SLIME_BLOW);
        sfx.play_SFX(sfx.BUBBLE_POP);

        float speedLesser = 0;
        if (difficulty == 1) speedLesser = 0.5f;
        if (difficulty == 2) speedLesser = 1.5f;
        if (difficulty == 3) speedLesser = 2.5f;
        float homeDuration = 3f;
        if (phase2)
        {
            homeDuration = 5f;
            attackIndex += 5;
        }
        // Spawn the projectile directly in front of the boss
        GameObject projectile = Instantiate(skillPrefabs[attackIndex], bossPosition.position, Quaternion.identity);

        // Get the HomingProjectile component and configure its target
        HomingProjectile homingProjectile = projectile.AddComponent<HomingProjectile>();
        homingProjectile.target = playerPosition;
        homingProjectile.speed = AttackSpeed - speedLesser; // Adjust speed as necessary
        homingProjectile.homingDuration = homeDuration; // Time for the projectile to follow the target
        homingProjectile.destroyAfter = 30f; // Time before the projectile self-destructs
    }

    private IEnumerator SpawnStompLine(int attackIndex)
    {
        sfx.play_SFX(sfx.SLIME_SPLASH);
        int numPrefabs = 8; // Number of prefabs to spawn
        float distanceBetween = 1f; // Distance between each prefab
        float spawnDelay = 0.5f; // Delay between each spawn (1 or 1.5 seconds)
        int numbOfLines = 2;
        if (phase2)
        {
            numbOfLines = 4;
            attackIndex += 5;
        }
        for (int j = 0; j < numbOfLines; j++)
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
                stompPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace - 90f);

                // Destroy the prefab after 3 seconds
                Destroy(stompPrefab, 3f);

                // Wait for the specified delay before spawning the next prefab
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    private IEnumerator SpawnUltimateAttack(int attackIndex)
    {
        sfx.play_SFX(sfx.SLIME_SPLASH);
        float warningDuration = 2f; // Duration the warning stays visible
        float distanceBetween = 2f; // Distance between prefabs along the line
        float lineOffset = .1f; // Offset between parallel lines
        int numRows = 1; // Number of rows (wave segments) per line
        float waveDelay = 0.1f; // Delay between each wave (row spawn)
        List<GameObject> warningPrefabs = new List<GameObject>(); // Store warning prefab instances
        List<Vector3> warningPositions = new List<Vector3>(); // Store final spawn positions

        // Choose one pattern
        int pattern = Random.Range(0, 101); // 0 = 4 quadrants, 1 = horizontal + vertical
        if (pattern <= 50)
        {
            yield return SpawnWaveLines(warningPrefabs, warningPositions, numRows, distanceBetween, lineOffset, true, waveDelay);
        }
        else
        {
            yield return SpawnWaveLines(warningPrefabs, warningPositions, numRows, distanceBetween, lineOffset, false, waveDelay);
        }

        // Wait for warnings to clear
        yield return new WaitForSeconds(warningDuration);

        // Destroy warnings
        foreach (GameObject warningPrefab in warningPrefabs)
        {
            Destroy(warningPrefab);
        }

        // Spawn projectiles at the warning positions
        foreach (Vector3 position in warningPositions)
        {
            GameObject ultimatePrefab = Instantiate(skillPrefabs[attackIndex], position, Quaternion.identity);
            Vector3 direction = (position - bossPosition.position).normalized;
            float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ultimatePrefab.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace - 90f);
            ultimatePrefab.GetComponent<Rigidbody2D>().linearVelocity = direction * AttackSpeed;
            Destroy(ultimatePrefab, 4f);
        }
    }

    private IEnumerator SpawnWaveLines(
    List<GameObject> warningPrefabs,
    List<Vector3> warningPositions,
    int numRows,
    float distanceBetween,
    float lineOffset,
    bool isQuadrantMode,
    float waveDelay
)
    {
        // Define the direction vectors for the quadrants
        Vector3[] baseDirections = isQuadrantMode
            ? new Vector3[]
              {
              (Vector3.up + Vector3.right).normalized,   // Top-right
              (Vector3.up + Vector3.left).normalized,    // Top-left
              (Vector3.down + Vector3.right).normalized, // Bottom-right
              (Vector3.down + Vector3.left).normalized   // Bottom-left
              }
            : new Vector3[]
              {
              Vector3.up,    // Vertical (upward)
              Vector3.down,  // Vertical (downward)
              Vector3.right, // Horizontal (rightward)
              Vector3.left   // Horizontal (leftward)
              };

        // Step 1: Generate Warning Positions
        foreach (Vector3 baseDirection in baseDirections)
        {
            for (int row = 0; row < numRows; row++) // Wave rows
            {
                for (int line = -1; line <= 1; line++) // Three lines: -1 (left/top), 0 (center), 1 (right/bottom)
                {
                    // Calculate line offset
                    Vector3 offsetDirection = Vector3.Cross(baseDirection, Vector3.forward).normalized;
                    Vector3 lineOffsetVector = offsetDirection * line * lineOffset;

                    // Calculate warning impact position
                    Vector3 impactPosition = bossPosition.position +
                                             baseDirection * distanceBetween * (row + 1) +
                                             lineOffsetVector;

                    // Store the warning position for projectiles later
                    warningPositions.Add(impactPosition);

                    // Instantiate warning prefab
                    GameObject warningPrefab = Instantiate(warning, impactPosition, Quaternion.identity);

                    // Store the warning instance for destruction later
                    warningPrefabs.Add(warningPrefab);
                }

                // Wait between rows to create the wave effect
                yield return new WaitForSeconds(waveDelay);
            }
        }

        // Wait for warnings to clear before spawning wave projectiles
        yield return null; // Return control after warnings are generated
    }

}