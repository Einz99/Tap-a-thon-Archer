using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToShop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnBackToShop()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
