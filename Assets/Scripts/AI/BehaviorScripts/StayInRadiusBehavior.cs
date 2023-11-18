using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/StayInRadius")]
public class StayInRadiusBehavior : FlockBehaviour
{
    [SerializeField] float radius;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 moveDirection = (Vector2)flock.transform.position - (Vector2)agent.transform.position;
        float distanceFactor = moveDirection.magnitude / radius;
        if (distanceFactor < 0.9f)
        {
            return Vector2.zero;
        }
        else
        {
            return moveDirection * distanceFactor * distanceFactor;
        }
    }
}
