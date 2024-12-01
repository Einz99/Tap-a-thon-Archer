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
        PlayerPrefs.SetInt(sceneCheckerkey, 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }

    void OnDestroy()
    {
        if(videoPlayer != null)
        {
            videoPlayer.loopPointReached -= onVideoEnd;
        }
    }
}
