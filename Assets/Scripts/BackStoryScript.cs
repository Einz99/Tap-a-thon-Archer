using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BackStoryScript : MonoBehaviour
{
    private const string sceneCheckerkey = "sceneCheck";
    public VideoPlayer videoPlayer;
    public Scene scene;
    void Start()
    {
        videoPlayer.loopPointReached += onVideoEnd;
    }

    public void onVideoEnd(VideoPlayer vp) 
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt(sceneCheckerkey, 1);
    }

    void OnDestroy()
    {
        if(videoPlayer != null)
        {
            videoPlayer.loopPointReached -= onVideoEnd;
        }
    }
}
