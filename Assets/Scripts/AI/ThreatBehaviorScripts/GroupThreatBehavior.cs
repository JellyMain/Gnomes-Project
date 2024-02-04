using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ThreatBehavior/GroupThreatBehavior")]
public class GroupThreatBehavior : ThreatBehavior
{
    [SerializeField] float threatTime = 3f;

    public override Vector2 CalculateMove(Fish fish, Flock flock, List<Transform> filteredNeighbors, Transform dangerObject)
    {
        Vector2 moveDirection = -(dangerObject.position - fish.transform.position).normalized;

        fish.ThreatTimer += Time.fixedDeltaTime;

        if (fish.ThreatTimer >= threatTime)
        {
            fish.ChangeBehaviorState(BehaviorState.Peaceful);
            fish.WasThreaten = false;
            fish.ThreatTimer = 0;
        }


        foreach (Transform neighbor in filteredNeighbors)
        {
            if (neighbor != null)
            {
                Fish neighborFish = neighbor.GetComponent<Fish>();
                if (!neighborFish.WasThreaten)
                {
                    neighborFish.SetThreatObject(dangerObject);
                    neighborFish.ChangeBehaviorState(BehaviorState.Threat);
                    neighborFish.WasThreaten = true;
                }
            }
        }

        return moveDirection;
    }
}
