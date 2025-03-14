using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private List<Transform> spawnPoints;

    public NavMeshSurface surface2d;

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject newBuilding = Instantiate(buildings[Random.Range(0, buildings.Count)], spawnPoints[i]);
            newBuilding.transform.Rotate(new Vector3(0, 0, Random.Range(0, 4) * 90));
        }

        surface2d.BuildNavMeshAsync();
    }
}
