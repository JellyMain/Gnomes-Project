using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlockBehavior/Composite")]
public class CompositeBehavior : FlockBehaviour
{
    public FlockBehaviour[] behaviors;
    public float[] weights;



    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 compositeMoveDirection = Vector2.zero;


        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 moveDirection = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

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
