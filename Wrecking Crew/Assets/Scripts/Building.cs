using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private GameObject collider2d;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private GameObject explosionParticles;

    [SerializeField] private int health = 3;
 
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody colliderRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (colliderRigidbody != null)
        {
            Debug.Log(colliderRigidbody.velocity.magnitude);
            if (colliderRigidbody.velocity.magnitude > 10)
            {
                health -= 1;
            }
            else if (colliderRigidbody.velocity.magnitude > 20) {
                health -= 2;
            }
            else if (colliderRigidbody.velocity.magnitude > 30)
            {
                health -= 3;
            }
        }

        if (health <= 0) DestroyBuilding();

    }

    private void DestroyBuilding()
    {
        destroyParticles.SetActive(true);
        explosionParticles.SetActive(true);

        collider2d.SetActive(false);
        gameObject.SetActive(false);
    }
}
