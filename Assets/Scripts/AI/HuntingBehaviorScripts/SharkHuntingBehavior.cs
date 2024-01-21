using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HuntingBehavior/SharkHunting")]
public class SharkHuntingBehavior : HuntingBehavior
{
    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {
        Vector2 targetPoint = context[0].transform.position;

        Vector2 directon = (targetPoint - (Vector2)fish.transform.position).normalized;

        return directon;

    }
}
