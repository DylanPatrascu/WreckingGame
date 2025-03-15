using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBounce : MonoBehaviour
{

    [SerializeField] float bounceStrength = 5000.0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Vector2 dir = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = dir * bounceStrength;
        }
    }
}
