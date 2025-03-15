using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoliceSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> policeSpawnPoint;
    [SerializeField] float startingPoliceSpawnRate;
    [SerializeField] private float spawnAcceleration;

    [SerializeField] GameObject policeCar;

    private float spawnRate;

    private void Start()
    {
        spawnRate = startingPoliceSpawnRate;
        StartCoroutine(SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        while (true) 
        {
            Instantiate(policeCar, policeSpawnPoint[Random.Range(0, policeSpawnPoint.Count)]);
            yield return new WaitForSeconds(spawnRate);
            spawnRate -= spawnAcceleration;
        }
        
    }

}
