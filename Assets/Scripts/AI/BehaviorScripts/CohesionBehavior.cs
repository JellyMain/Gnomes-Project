using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
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
            Vector2 cohesionPoint = Vector2.zero;


            foreach (Transform contextAgent in filteredAgents)
            {
                cohesionPoint += (Vector2)contextAgent.transform.position;
            }

            cohesionPoint /= filteredAgents.Count;

            Vector2 directon = cohesionPoint - (Vector2)agent.transform.position;

            return directon.normalized;
        }



    }
}
