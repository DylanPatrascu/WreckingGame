using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] private GameObject c2D;
    [SerializeField] private ParticleSystem destroyParticles;
    [SerializeField] GameLogic gameLogic;


    private void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
        destroyParticles.Pause();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            destroyParticles.Play();
            GetComponent<Collider>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            c2D.SetActive(false);

            //gameLogic.AddTime(time);
        }
    }

    private void OnAreaEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            destroyParticles.Play();
            GetComponent<Collider>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            c2D.SetActive(false);

            //gameLogic.AddTime(time);
        }
    }
}
