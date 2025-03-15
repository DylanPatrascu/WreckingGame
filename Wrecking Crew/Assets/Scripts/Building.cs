using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private GameObject collider2d;
    [SerializeField] private List<ParticleSystem> destroyParticles;
    [SerializeField] private SpriteRenderer buildingSprite;
    [SerializeField] private Sprite[] damageSprites;

    [SerializeField] private int health = 3;
    [SerializeField] private float time = 3;

    [SerializeField] GameLogic gameLogic;

    [SerializeField] private List<AudioClip> bigBooms;

    private int maxHealth;
    private AudioManager audioManager;

    private void Start()
    {
        JunkMeter.maxProgress += 1;
        maxHealth = health;
        gameLogic = FindAnyObjectByType<GameLogic>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Balls");
            Rigidbody colliderRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log(Ball.prevVelocity);
            if (colliderRigidbody != null)
            {
                if (Ball.prevVelocity > 75)
                {
                    health -= 3;

                    Debug.Log("BIIIIG hit: " + Ball.prevVelocity.ToString());
                }
                else if (Ball.prevVelocity > 50)
                {
                    health -= 2;
                    Debug.Log("Medium hit: " + Ball.prevVelocity.ToString());
                }
                else if (Ball.prevVelocity > 25)
                {
                    health -= 1;
                    Debug.Log("Small hit: " + Ball.prevVelocity.ToString());
                }
                
            }

            if (health > 0)
            {
                float ratio = (float)health / maxHealth;

                if (ratio > 0.75f) buildingSprite.sprite = damageSprites[0];
                else if (ratio > 0.50f) buildingSprite.sprite = damageSprites[1];
                else if (ratio > 0.25f) buildingSprite.sprite = damageSprites[2];
                else buildingSprite.sprite = damageSprites[3];
            }

            if (health <= 0) DestroyBuilding();

        }
    }

    private void DestroyBuilding()
    {
        foreach(ParticleSystem particle in destroyParticles)
        {
            particle.Play();
        }
        JunkMeter.progress += 1;
        audioManager.PlayRandomSound(bigBooms);

        buildingSprite.enabled = false;

        collider2d.SetActive(false);
        gameObject.SetActive(false);

        gameLogic.AddTime(time);
    }
}
