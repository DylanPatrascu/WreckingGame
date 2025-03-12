using UnityEngine;

public class BirdScroller : MonoBehaviour
{
    [SerializeField] private RectTransform birdsRect;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;

    private void Start()
    {
        if (birdsRect)
        {
            birdsRect.anchoredPosition = new Vector2(leftX, birdsRect.anchoredPosition.y);
        }
    }

    private void Update()
    {
        if (!birdsRect) return;
        Vector2 pos = birdsRect.anchoredPosition;
        pos.x += scrollSpeed * Time.deltaTime;
        if (pos.x > rightX) pos.x = leftX;
        birdsRect.anchoredPosition = pos;
    }
}
