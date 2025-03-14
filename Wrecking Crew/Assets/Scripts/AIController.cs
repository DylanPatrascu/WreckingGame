using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] private GameObject c2D;
    [SerializeField] private List<ParticleSystem> destroyParticles;
    [SerializeField] private float time = 1;
    [SerializeField] private float speed = 5;
    [SerializeField] private float acceleraion = 4;

    private Vector3 prevPosition;

    private bool dead = false;
    

    private void Start()
    {
        agent.speed = speed;
        agent.acceleration = acceleraion;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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

            gameLogic.AddTime(time);
        }
    }

}
