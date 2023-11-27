using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] Fish agentPrefab;
    [SerializeField] float agentDensity = 0.08f;
    [SerializeField, Range(10, 100)] float flockRadius = 20f;
    [SerializeField, Range(20, 500)] int startingCount = 250;
    [SerializeField, Range(1, 100)] float driveFactor = 10f;
    [SerializeField, Range(1, 100)] float maxSpeed = 5f;

    public float FlockRadius => flockRadius;


    private void Start()
    {
        SpawnFlock();
    }


    private void SpawnFlock()
    {
        for (int i = 0; i < startingCount; i++)
        {
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * startingCount * agentDensity;
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

            Fish spawnedAgent = Instantiate(agentPrefab, randomPosition, randomRotation, transform);

            spawnedAgent.Init(this);

            spawnedAgent.name = "Agent" + i;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, flockRadius);
    }


}
