using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThreatBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(Fish fish, Flock flock, List<Transform> filteredNeighbors, Transform dangerObject);
}
