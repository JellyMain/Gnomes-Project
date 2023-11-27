using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(Fish agent, List<Transform> context, List<Transform> filteredContext, Flock flock);
}
