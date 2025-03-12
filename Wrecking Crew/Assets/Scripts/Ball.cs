using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform link;
    [SerializeField] private ParticleSystem hitParticle;

    public static event Action OnBallHit;

    private void Update()
    {
        transform.position = link.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitParticle.Play();
        OnBallHit.Invoke();
    }

}
