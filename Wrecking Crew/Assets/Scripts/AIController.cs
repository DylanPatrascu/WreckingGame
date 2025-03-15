using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] private GameObject c2D;
    [SerializeField] private List<ParticleSystem> destroyParticles;
    [SerializeField] private float time = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private float acceleraion = 4;

    [SerializeField] private AudioSource source;
    [SerializeField] private float distanceThreshhold;
    [SerializeField] private AudioClip sirenClip;
    [SerializeField] private AudioClip policeDeathClip;

    private Vector3 prevPosition;

    private bool dead = false;
    

    private void Start()
    {
        agent.speed = speed;
        agent.acceleration = acceleraion;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = FindObjectOfType<PlayerControls>().transform;
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void Update()
    {
        if (GameLogic.gameRunning && !dead)
        {
            agent.SetDestination(target.position);

            if (prevPosition != null && prevPosition != transform.position)
            {
                Vector3 dir = (transform.position - prevPosition).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            prevPosition = transform.position;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < distanceThreshhold)
        {
            source.volume = 1 - Vector2.Distance(transform.position, target.position) / distanceThreshhold;
        }
        else
        {
            source.volume = 0;
        }
        
        
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
