using UnityEngine;

public class FightCalculation : MonoBehaviour
{
    public PlayersPreferables PP;
    private const string difficultyKey = "difficulty";
    public GameObject hearts;
    public GameObject extrahearts;
    private int extralife;
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        switch (difficulty)
        {
            case 1: 
                extralife = 2;
                ;
            break;
            case 2: 
                extralife = 3;
                ;
            break;
            case 3: 
                extralife = 4;
                ;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
