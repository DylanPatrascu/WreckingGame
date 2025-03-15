using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform link;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] AudioManager manager;
    [SerializeField] List<AudioClip> ballHitSound;

    public static event Action OnBallHit;
    public static float prevVelocity;

    private void Start()
    {
        manager = FindFirstObjectByType<AudioManager>();
        StartCoroutine(SetPrevVelocity());
    }

    private void Update()
    {
        transform.position = link.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitParticle.Play();
        manager.PlaySound(ballHitSound[UnityEngine.Random.Range(0, ballHitSound.Count)]);
        OnBallHit.Invoke();
    }

    public IEnumerator SetPrevVelocity()
    {
        while (GameLogic.gameRunning)
        {
            prevVelocity = GetComponent<Rigidbody>().velocity.magnitude;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
