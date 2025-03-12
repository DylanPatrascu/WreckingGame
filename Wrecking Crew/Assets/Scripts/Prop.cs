using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] private GameObject collider2d;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private SpriteRenderer propSprite;
    [SerializeField] GameLogic gameLogic;


    private void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collide");

        DestroyProp();
    }

    private void DestroyProp()
    {
        destroyParticles.SetActive(true);
        propSprite.enabled = false;

        collider2d.SetActive(false);
        gameObject.SetActive(false);

        gameLogic.AddTime(2);
    }
}
