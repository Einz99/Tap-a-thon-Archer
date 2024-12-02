using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBossFightScript : MonoBehaviour
{
    private const string difficultyKey = "difficulty";
    
    public void OnPressDifficulty(int difficulty) 
    {
        PlayerPrefs.SetInt(difficultyKey, difficulty);
        PlayerPrefs.Save();
        int randomBoss = Random.Range(3,8);
        SceneManager.LoadScene(3);
    }
}
