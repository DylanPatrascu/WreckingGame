using UnityEngine;
using UnityEngine.UI;

public class JunkMeter : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    [SerializeField] private CanvasScaler scaler;

    [SerializeField] public static int progress = 0;
    [SerializeField] public static int maxProgress = 0;

    private void Start()
    { 
        bar.offsetMax = new Vector2(-scaler.referenceResolution.x, bar.offsetMax.y);
    }

    private void Update()
    {
        float ratio = 1 - ((float)progress / (float)maxProgress);
        bar.offsetMax = new Vector2(-scaler.referenceResolution.x * ratio, bar.offsetMax.y);
    }

}
