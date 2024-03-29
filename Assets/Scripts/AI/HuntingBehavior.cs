using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HuntingBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(Fish fish, Fish prey);
}
