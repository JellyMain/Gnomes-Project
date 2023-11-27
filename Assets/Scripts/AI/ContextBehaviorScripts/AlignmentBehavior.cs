using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ContextBehavior/Alignment")]
public class AlignmentBehavior : ContextBehavior
{
    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {

        Vector2 alignmentPoint = Vector2.zero;

        foreach (Transform contextAgent in filteredContext)
        {
            alignmentPoint += (Vector2)contextAgent.transform.right;
        }

        alignmentPoint /= context.Count;

        return alignmentPoint;

    }
}
