using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Debug.Log(bar.offsetMax);
    }

    private void Update()
    {
        float ratio = ((float)progress / (float)maxProgress);
        bar.offsetMax = new Vector2(-scaler.referenceResolution.x * ratio, bar.offsetMax.y);
        Debug.Log(progress / maxProgress);
        Debug.Log(new Vector2(ratio, -scaler.referenceResolution.x * ratio));
    }

}
