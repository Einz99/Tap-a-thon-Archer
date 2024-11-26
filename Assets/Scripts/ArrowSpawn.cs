using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public GameObject prefab; 
    public Transform target;  
    public float spawnRate; 
    public float speed; 

    private void Start()
    {
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
