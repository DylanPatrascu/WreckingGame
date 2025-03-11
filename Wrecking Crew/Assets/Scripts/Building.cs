using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private GameObject collider2d;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private SpriteRenderer buildingSprite;
    [SerializeField] private Sprite[] damageSprites;  

    [SerializeField] private int health = 10;
    [SerializeField] private int spriteIndex = 0;

    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ball")
        {

            Rigidbody colliderRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (colliderRigidbody != null)
            {
                if (colliderRigidbody.velocity.magnitude > 10)
                {
                    health -= 1;
                }
                else if (colliderRigidbody.velocity.magnitude > 20)
                {
                    health -= 2;
                }
                else if (colliderRigidbody.velocity.magnitude > 30)
                {
                    health -= 3;
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
        destroyParticles.SetActive(true);
        explosionParticles.SetActive(true);

        buildingSprite.enabled = false;

        collider2d.SetActive(false);
        gameObject.SetActive(false);
    }
}
