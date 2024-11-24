using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BackStoryScript : MonoBehaviour
{
    private const string sceneCheckerkey = "sceneCheck";
    public VideoPlayer videoPlayer;
    public Scene scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.loopPointReached += onVideoEnd;
    }

    public void onVideoEnd(VideoPlayer vp) 
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt(sceneCheckerkey, 1);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if(videoPlayer != null)
        {
            videoPlayer.loopPointReached -= onVideoEnd;
        }
    }
}
