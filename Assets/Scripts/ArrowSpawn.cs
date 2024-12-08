using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public GameObject prefab; 
    public GameObject critPrefab; 
    public Transform target;
    public PlayerCalculation PC;
    public BossHealthBar BHB;
    private float spawnRate;
    private float holdingSR;

    public MasterVolume sfx;

    private void Start()
    {   
        spawnRate = PC.spawnRate;
        checkHolding(false);
        holdingSR = PC.HoldingSR;
    }
    
    
    public void checkHolding(bool isHolding)
    {
        if(isHolding)
        {
            CancelInvoke(nameof(checkCrit));
            InvokeRepeating(nameof(checkCrit), 0f, spawnRate - holdingSR);
            // sfx.play_SFX(sfx.BOW_CHARGE);
        }
        else
        {
            CancelInvoke(nameof(checkCrit));
            InvokeRepeating(nameof(checkCrit), 0f, spawnRate);
        }
    }
        
    private void checkCrit()
    {
        int randomCrit = Random.Range(1,101);
        if(randomCrit > PC.criticalChance)
        {
            SpawnRegPrefab();
            sfx.play_SFX(sfx.SHOOTING_ARROW);
            BHB.damage = PC.attackPower;
        }
        else
        {
            SpawnCritPrefab();
            sfx.play_SFX(sfx.SHOOTING_ARROW);
            sfx.play_SFX(sfx.CRIT_ARROW);
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
