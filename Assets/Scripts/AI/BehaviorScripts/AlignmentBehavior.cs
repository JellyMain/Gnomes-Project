using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredAgents = (agentFilter == null) ? context : agentFilter.GetFilteredAgents(agent, context);

        if (filteredAgents.Count == 0)
        {
            return agent.transform.right;
        }
        else
        {
            Vector2 alignmentPoint = Vector2.zero;



            foreach (Transform contextAgent in filteredAgents)
            {
                alignmentPoint += (Vector2)contextAgent.transform.right;
            }

            alignmentPoint /= context.Count;

            return alignmentPoint;
        }
    }
}
