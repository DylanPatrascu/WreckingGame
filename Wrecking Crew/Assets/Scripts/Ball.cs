using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform link;
    [SerializeField] private ParticleSystem hitParticle;

    private void Update()
    {
        transform.position = link.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitParticle.Play();
    }

}
