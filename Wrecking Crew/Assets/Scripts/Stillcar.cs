using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stillcar : MonoBehaviour
{
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private GameObject c2D;
    [SerializeField] private List<ParticleSystem> destroyParticles;
    [SerializeField] private float time = 1;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip policeDeathClip;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && !dead)
        {
            foreach (ParticleSystem particle in destroyParticles)
            {
                particle.Play();
            }

            GetComponent<Collider>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            c2D.SetActive(false);
            dead = true;
            source.Stop();
            source.PlayOneShot(policeDeathClip);
            gameLogic.AddTime(time);
        }
    }

}

