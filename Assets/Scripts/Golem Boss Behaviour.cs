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
        animator.SetBool("Beary_Transform", true);
        yield return new WaitForSeconds(2f); // Wait for the transformation animation duration

        // Switch to the idle animation after the transformation
        animator.SetBool("Beary_Transform", false);
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
            case 0: Debug.Log("Golem_Bullet"); return "Golem_Bullet";
            case 1: Debug.Log("Golem_Charge"); return "Golem_Charge";
            case 2: Debug.Log("Golem_Crystal"); return "Golem_Crystal";
            case 3: Debug.Log("Golem_Shield"); return "Golem_Shield";
            case 4: Debug.Log("Golem_Slam"); return "Golem_Slam";
            case 5: Debug.Log("Golem_Bullet"); return "Golem_Bullet";
            case 6: Debug.Log("Golem_Charge"); return "Golem_Charge";
            case 7: Debug.Log("Golem_Crystal"); return "Golem_Crystal";
            case 8: Debug.Log("Golem_Shield"); return "Golem_Shield";
            case 9: Debug.Log("Golem_Slam"); return "Golem_Slam";
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

    

    private IEnumerator SpawnUltimateAttack(int attackIndex)
    {
        // Step 1: Spawn the warning prefab around the boss
        float warningDuration = 2f; // Duration the warning stays visible
        float warningRadius = 4f; // Radius around the boss where warnings will spawn
        List<GameObject> warningPrefabs = new List<GameObject>(); // Store the warning prefab instances
        List<Vector3> warningPositions = new List<Vector3>(); // Store the positions for ultimate spawns
        int shockwaves = 10;
        if (phase2)
        {
            shockwaves = 20;
            attackIndex += 5;
        }

        // Randomly spawn multiple warning prefabs around the boss and store their positions
        for (int i = 0; i < shockwaves; i++) // 10 warning prefabs, adjust as needed
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
            GameObject ultimatePrefab = Instantiate(skillPrefabs[attackIndex], position, Quaternion.identity);

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