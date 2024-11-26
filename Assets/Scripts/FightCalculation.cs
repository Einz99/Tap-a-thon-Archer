using Unity.VisualScripting;
using UnityEngine;

public class FightCalculation : MonoBehaviour
{
    public PlayersPreferables PP;
    private const string difficultyKey = "difficulty";
    public GameObject hearts;
    public GameObject extrahearts;
    private int extralife;
    private float attackspeed;
    private float bulletSpawnRate;
    private float Health;
    private bool isPhase2;
    private int weatherChance;

    void Start()
    {
        int difficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        switch (difficulty)
        {
            case 1: 
                extralife = 2;
                attackspeed = 0f;
                bulletSpawnRate = 0f;
                Health = 0f;
                isPhase2 = false;
                weatherChance = 0;
            break;
            case 2: 
                extralife = 3;
                attackspeed = 0f;
                bulletSpawnRate = 0f;
                Health = 0f;
                isPhase2 = false;
                weatherChance = 0;
            break;
            case 3: 
                extralife = 4;
                attackspeed = 0f;
                bulletSpawnRate = 0f;
                Health = 0f;
                isPhase2 = false;
                weatherChance = 0;
            break;
        }
        for (int i = 0; i < extralife; i++)
        {
            prefabInstantiate();
            Debug.Log("heart Added");
        }
    }

    private void prefabInstantiate()
    {
        GameObject instance = Instantiate(hearts);
        instance.transform.SetParent(extrahearts.transform);
    }

    void Update()
    {
        
    }
}
