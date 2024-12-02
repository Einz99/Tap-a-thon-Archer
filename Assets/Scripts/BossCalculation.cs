using Unity.VisualScripting;
using UnityEngine;

public class FightCalculation : MonoBehaviour
{
    private const string difficultyKey = "difficulty";
    public GameObject hearts;
    public GameObject extrahearts;
    private int extralife;
    public float attackspeed;
    public float Health;
    private bool isPhase2;
    private int weatherChance;
    public BossHealthBar BHB;

    void Start()
    {
        Time.timeScale = 1;
        int difficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        switch (difficulty)
        {
            case 1: 
                extralife = 2;
                attackspeed = 1.3f;
                Health = 300;
                isPhase2 = false;
                weatherChance = 0;
            break;
            case 2: 
                extralife = 3;
                attackspeed = 2.3f;
                Health = 450;
                isPhase2 = true;
                weatherChance = 15;
            break;
            case 3: 
                extralife = 4;
                attackspeed = 3.3f;
                Health = 600;
                isPhase2 = true;
                weatherChance = 30;
            break;
        }
        for (int i = 0; i < extralife + 1; i++)
        {
            prefabInstantiate();
            Debug.Log("heart Added");
        }
        BHB.MaxHealth = Health;

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
