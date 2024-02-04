using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HuntingBehavior/SharkHunting")]
public class SharkHuntingBehavior : HuntingBehavior
{
    public override Vector2 CalculateMove(Fish fish, Fish prey)
    {
        if (prey == null)
        {
            fish.ChangeBehaviorState(BehaviorState.Peaceful);
            return Vector2.zero;
        }

        Vector2 directon = (prey.transform.position - fish.transform.position).normalized;

        return directon;
    }
}
