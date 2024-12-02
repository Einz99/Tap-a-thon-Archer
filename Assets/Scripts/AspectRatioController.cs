using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    private void Start()
    {
        // Target aspect ratio: 5:4
        float targetAspect = 5.0f / 4.0f;

        // Get current screen aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;

        // Calculate scale factor
        float scaleHeight = windowAspect / targetAspect;

        // Adjust the viewport
        if (scaleHeight < 1.0f)
        {
            Rect rect = Camera.main.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = Camera.main.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            Camera.main.rect = rect;
        }
    }
}