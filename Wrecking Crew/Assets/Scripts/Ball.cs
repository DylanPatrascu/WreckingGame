using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform link;

    private void Update()
    {
        transform.position = link.position;
    }
}
