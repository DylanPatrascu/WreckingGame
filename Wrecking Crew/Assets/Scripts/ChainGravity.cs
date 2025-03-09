using UnityEngine;

public class Chaingravity : MonoBehaviour
{
    Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }
    
    void Update()
    {
        transform.rotation = initialRotation;
    }
}
