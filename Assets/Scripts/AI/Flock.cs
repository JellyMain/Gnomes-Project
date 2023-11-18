using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] FlockAgent agentPrefab;
    [SerializeField] FlockBehaviour flockBehaviour;
    [SerializeField, Range(20, 500)] int startingCount = 250;
    [SerializeField, Range(1, 100)] float driveFactor = 10f;
    [SerializeField, Range(1, 100)] float maxSpeed = 5f;
    [SerializeField, Range(1, 10)] float neighborRadius = 1.5f;
    [SerializeField, Range(0, 1)] float avoidanceRadiusMultiplier = 0.5f;
    private List<FlockAgent> agentList = new List<FlockAgent>();

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;

    public float SquareAvoidanceRadius => squareAvoidanceRadius;
    public float NeighborRadius => neighborRadius;

    [SerializeField] float agentDensity = 0.08f;


    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        SpawnFlock();
    }


    private void Update()
    {
        IterateFlock();
    }



    private List<Transform> GetNeighbors(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        foreach (Collider2D contextCollider in contextColliders)
        {
            if (contextCollider != agent.AgentCollider)
            {
                context.Add(contextCollider.transform);
            }
        }
        return context;
    }


    private void SpawnFlock()
    {
        for (int i = 0; i < startingCount; i++)
        {
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * startingCount * agentDensity;
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

            FlockAgent spawnedAgent = Instantiate(agentPrefab, randomPosition, randomRotation, transform);

            spawnedAgent.Init(this);

            spawnedAgent.name = "Agent" + i;

            agentList.Add(spawnedAgent);
        }
    }



    private void IterateFlock()
    {
        foreach (FlockAgent agent in agentList)
        {
            List<Transform> context = GetNeighbors(agent);
            Vector2 move = flockBehaviour.CalculateMove(agent, context, this);

            Vector2 clampedMove = Vector2.ClampMagnitude(move * driveFactor, squareMaxSpeed);

            agent.Move(clampedMove);
        }
    }

}
