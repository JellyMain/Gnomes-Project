using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ThreatBehavior/StandartThreatBehavior")]
public class StandartThreatBehavior : ThreatBehavior
{
    [SerializeField] float threatTime = 3f;

    public override Vector2 CalculateMove(Fish fish, Flock flock, List<Transform> filteredNeighbors, Transform dangerObject)
    {
        Vector2 moveDirection = -(dangerObject.position - fish.transform.position).normalized;

        fish.ThreatTimer += Time.fixedDeltaTime;

        if (fish.ThreatTimer >= threatTime)
        {
            fish.ChangeBehaviorState(BehaviorState.Peaceful);
            fish.ThreatTimer = 0;
        }

        return moveDirection;
    }
}
