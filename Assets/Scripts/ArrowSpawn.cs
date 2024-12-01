using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public GameObject prefab; 
    public GameObject critPrefab; 
    public Transform target;
    public PlayerCalculation PC;
    public BossHealthBar BHB;
    private void Start()
    {   
        checkHolding(false);
    }
    
    
    public void checkHolding(bool isHolding)
    {
        if(isHolding)
        {
            CancelInvoke(nameof(checkCrit));
        }
        else
        {
            InvokeRepeating(nameof(checkCrit), 0f, PC.spawnRate);
        }
    }
        
    private void checkCrit()
    {
        int randomCrit = Random.Range(1,101);
        if(randomCrit > PC.criticalChance)
        {
            SpawnRegPrefab();
            BHB.damage = PC.attackPower;
        }
        else
        {
            SpawnCritPrefab();
            BHB.damage = PC.attackPower * PC.criticalMultiplier;
        }
    }

    private void SpawnRegPrefab()
    {
        GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
        ArrowDirections arrowDirections = spawnedObject.AddComponent<ArrowDirections>();
        arrowDirections.target = target;
        arrowDirections.speed = PC.attackSpeed;
    }
    private void SpawnCritPrefab()
    {
        GameObject spawnedObject = Instantiate(critPrefab, transform.position, Quaternion.identity);
        ArrowDirections arrowDirections = spawnedObject.AddComponent<ArrowDirections>();
        arrowDirections.target = target;
        arrowDirections.speed = PC.attackSpeed;
    }
}
