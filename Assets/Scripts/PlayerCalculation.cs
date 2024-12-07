using UnityEngine;

public class PlayerCalculation : MonoBehaviour
{
    public int MaxHealth;
    public float attackPower;
    public float spawnRate;
    public float HoldingSR;
    public float attackSpeed;
    public float moveSpeed;
    public float criticalMultiplier;
    public float criticalChance;
    public float goldMultiplier;
    public PlayersPreferables PP;

    public const string currencyKey = "currency";
    public const string maxHealthKey = "maxHealth";
    public const string attackPowerKey = "attackPower";
    public const string attackSpeedKey = "attackSpeed";
    public const string moveSpeedKey = "moveSpeed";
    public const string critMultKey = "criticalMultiplier";
    public const string critChanceKey = "criticalChance";
    public const string goldMultKey = "goldMultiplier";

    public GameObject hearts;
    public GameObject heartsContainer;
    // Stats
    //      MaxHP = base 5, multiplicand 1 x 5
    //      Attack = base 10, multiplicand 2 x 10
    //      spawn rate = base 1, multiplicand .05 x 10
    //      attack speed = base 1 multiplicand 0.1 x 10
    //      Movespeed = 1.5 - 2.5 | base 1.5, multiplicand 0.2 x 5
    //      crit = base 1.5, multiplicand .2 x 10
    //      crit chance = base 10%, multiplicand 2%
    void Start()
    {
        MaxHealth = 5 + (1 * PlayerPrefs.GetInt(maxHealthKey, 0));
        attackPower = 10 + (2 * PlayerPrefs.GetInt(attackPowerKey, 0));
        attackSpeed = 1 + (0.1f * PlayerPrefs.GetInt(attackSpeedKey, 0));
        spawnRate = 1 - (.03f * PlayerPrefs.GetInt(attackSpeedKey, 0));
        HoldingSR = 0.015f * PlayerPrefs.GetInt(attackSpeedKey, 0);
        moveSpeed = 1.5f + (0.2f * PlayerPrefs.GetInt(moveSpeedKey, 0));
        criticalMultiplier = 1.5f + (0.2f * PlayerPrefs.GetInt(critMultKey, 0));
        criticalChance = 10 + (2 * PlayerPrefs.GetInt(critChanceKey, 0));
        for (int i = 0; i < MaxHealth; i++)
        {
            prefabInstantiate();
        }
    }

    private void prefabInstantiate()
    {
        GameObject instance = Instantiate(hearts);
        instance.transform.SetParent(heartsContainer.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
