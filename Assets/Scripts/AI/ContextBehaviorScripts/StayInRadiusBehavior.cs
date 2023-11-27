using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ContextBehavior/StayInRadius")]
public class StayInRadiusBehavior : ContextBehavior
{

    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {
        Vector2 moveDirection = (Vector2)flock.transform.position - (Vector2)fish.transform.position;
        float distanceFactor = moveDirection.magnitude / flock.FlockRadius;
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
