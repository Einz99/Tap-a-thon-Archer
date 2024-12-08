using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class IdleVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public RawImage idleVideoRawImage; // Reference to the Raw Image
    public float idleTime = 60f; // Time in seconds before idle video starts

    private float idleTimer = 0f;
    private bool isVideoPlaying = false;

    void Start()
    {
        if (videoPlayer == null || idleVideoRawImage == null)
        {
            Debug.LogError("VideoPlayer or RawImage is not assigned!");
            return;
        }

        // Ensure the video and Raw Image are not visible initially
        videoPlayer.Stop();
        idleVideoRawImage.enabled = false;
    }

    void Update()
    {
        // Detect user input (mouse movement or keyboard press)
        if (Input.anyKey || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            idleTimer = 0f; // Reset idle timer
            if (isVideoPlaying)
            {
                StopVideo();
            }
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTime && !isVideoPlaying)
            {
                PlayVideo();
            }
        }
    }

    private void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            idleVideoRawImage.enabled = true; // Enable the Raw Image to display the video
            isVideoPlaying = true;
        }
    }

    private void StopVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            idleVideoRawImage.enabled = false; // Hide the Raw Image
            isVideoPlaying = false;
        }
    }
}
