using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/Filter/SameFlock")]
public class SameFlockFilter : AgentFilter
{
    public override List<Transform> GetFilteredAgents(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        for (int i = 0; i < original.Count; i++)
        {
            if (original[i].TryGetComponent(out FlockAgent flockAgent))
            {
                if (agent.Flock == flockAgent.Flock)
                {
                    filtered.Add(flockAgent.transform);
                }
            }
        }
        return filtered;
    }
}
