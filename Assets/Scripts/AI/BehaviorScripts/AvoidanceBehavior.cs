using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredAgents = (agentFilter == null) ? context : agentFilter.GetFilteredAgents(agent, context);

        if (filteredAgents.Count == 0)
        {
            return Vector2.zero;
        }
        else
        {
            int nAvoid = 0;
            Vector2 avoidancePoint = Vector2.zero;

            foreach (Transform contextAgent in filteredAgents)
            {
                if ((contextAgent.position - agent.transform.position).sqrMagnitude < flock.SquareAvoidanceRadius)
                {
                    nAvoid++;
                    avoidancePoint += (Vector2)(agent.transform.position - contextAgent.transform.position);
                }
            }

            if (nAvoid > 0)
            {
                avoidancePoint /= nAvoid;
            }


            return avoidancePoint;
        }
    }


}
