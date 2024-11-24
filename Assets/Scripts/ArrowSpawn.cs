using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public Transform target;  // The target the prefab will move towards
    public float spawnRate; // Spawn rate in seconds
    public float speed; // Speed of the prefab

    private void Start()
    {
        // Start spawning at fixed intervals
        InvokeRepeating(nameof(SpawnPrefab), 0f, spawnRate);
    }

    private void SpawnPrefab()
    {
        // Spawn the prefab at the spawner's position
        GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

        // Add a script to move the prefab
        ArrowDirections arrowDirections = spawnedObject.AddComponent<ArrowDirections>();
        arrowDirections.target = target;
        arrowDirections.speed = speed;
    }
}
