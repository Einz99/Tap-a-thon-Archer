using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ScrollRect scrollRect;
    public RectTransform content;
    public int pageCount;
    public float transitionSpeed;

    public float scrollAdjustments;

    public HorizontalLayoutGroup layoutGroup;

    private Vector2[] pagePositions;
    private Vector2 targetPosition;
    private void Start()
    {
        
    }

    private void Update()
    {
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.unscaledDeltaTime * transitionSpeed);
    }

    public void NavigateToPage(int pageIndex)
    {
        float spacing = layoutGroup.spacing;

        pagePositions = new Vector2[pageCount];
        float pageWidth = content.rect.width / pageCount;

        for (int i = 0; i < pageCount; i++)
        {
            pagePositions[i] = new Vector2(-i * (pageWidth + spacing) * scrollAdjustments, 0);
        }

        targetPosition = content.anchoredPosition;
        if (pageIndex < 0 || pageIndex >= pageCount)
        {
            Debug.LogError("Invalid page index!");
            return;
        }

        targetPosition = pagePositions[pageIndex];
    }
}
