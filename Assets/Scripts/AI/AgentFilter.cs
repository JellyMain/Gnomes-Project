using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentFilter : ScriptableObject
{
    public abstract List<Transform> GetFilteredAgents(Fish fish, List<Transform> original);
}
