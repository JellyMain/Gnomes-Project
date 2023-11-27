using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonContextBehavior : ScriptableObject
{
    public abstract void CalculateMove(Fish fish);
}
