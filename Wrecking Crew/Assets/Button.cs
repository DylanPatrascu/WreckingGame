using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    private Button b;

    private void OnEnable()
    {
        b = GetComponent<Button>();
        b.Select();
    }
}
