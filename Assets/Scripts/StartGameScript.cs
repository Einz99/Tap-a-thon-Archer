using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    
    private const string sceneCheckerkey = "sceneCheck";
    public Scene backstory;
    public Scene ShopScene;
    
    public void OnPressStart() 
    {
        if(!(PlayerPrefs.GetInt(sceneCheckerkey, 0) == 1))
        {
            SceneManager.LoadScene(1);
        }
        else {
            SceneManager.LoadScene(2);
        }
    }

    public void resetSceneChecker()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
