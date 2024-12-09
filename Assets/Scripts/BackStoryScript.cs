using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BackStoryScript : MonoBehaviour
{
    private const string sceneCheckerkey = "sceneCheck";
    public Scene scene;
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene() 
    {
        PlayerPrefs.SetInt(sceneCheckerkey, 1);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(34f); // 31 how long video + 4sec;
        SceneManager.LoadScene(2);
    }

    public void skip(){
        SceneManager.LoadScene(2);
    }


}
