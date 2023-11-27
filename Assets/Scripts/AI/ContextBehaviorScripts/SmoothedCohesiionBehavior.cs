using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ContextBehavior/SmoothedCohesion")]
public class SmoothedCohesiionBehavior : ContextBehavior
{
    [SerializeField] float smoothTime = 3;
    private Vector2 currentRefVelocity;

    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {
        Vector2 cohesionPoint = Vector2.zero;

        foreach (Transform contextAgent in filteredContext)
        {
            cohesionPoint += (Vector2)contextAgent.transform.position;
        }

        cohesionPoint /= filteredContext.Count;

        Vector2 direction = cohesionPoint - (Vector2)fish.transform.position;

        direction = Vector2.SmoothDamp(fish.transform.right, direction, ref currentRefVelocity, smoothTime);

        return direction.normalized;

    }
}
