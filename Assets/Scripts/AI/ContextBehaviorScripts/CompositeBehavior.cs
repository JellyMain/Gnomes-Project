using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ContextBehavior/Composite")]
public class CompositeBehavior : ContextBehavior
{
    public ContextBehavior[] behaviors;
    public float[] weights;


    public override Vector2 CalculateMove(Fish fish, List<Transform> context, List<Transform> filteredContext, Flock flock)
    {
        Vector2 compositeMoveDirection = Vector2.zero;


        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 moveDirection = behaviors[i].CalculateMove(fish, context, filteredContext, flock) * weights[i];

            if (moveDirection != Vector2.zero)
            {
                if (moveDirection.sqrMagnitude > weights[i] * weights[i])
                {
                    moveDirection.Normalize();
                    moveDirection *= weights[i];
                }

                compositeMoveDirection += moveDirection;
            }
        }

        return compositeMoveDirection;

    }
}
