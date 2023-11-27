using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ContextBehavior/Avoidance")]
public class AvoidanceBehavior : ContextBehavior
{

    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {
        int nAvoid = 0;
        Vector2 avoidancePoint = Vector2.zero;

        foreach (Transform contextAgent in filteredContext)
        {
            if ((contextAgent.position - fish.transform.position).sqrMagnitude < fish.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidancePoint += (Vector2)(fish.transform.position - contextAgent.transform.position);
            }
        }

        if (nAvoid > 0)
        {
            avoidancePoint /= nAvoid;
        }


        return avoidancePoint;

    }


}
