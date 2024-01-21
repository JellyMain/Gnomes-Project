using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonContextBehavior : ScriptableObject
{
    protected bool hasStarted = false;

    public void ResetStartingFlag()
    {
        hasStarted = false;
    }

    public abstract Vector2 CalculateMove(Fish fish);
}
