using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NonContextBehavior/TrapezoidSearch")]
public class TrapezoidRandomPointSearch : NonContextBehavior
{
    [SerializeField] float searchAreaBottomWidth = 4;
    [SerializeField] float searchAreaUpperWidth = 6;
    [SerializeField] float searchAreaHeight = 3;


    public override Vector2 CalculateMove(Fish fish)
    {
        CalculateSearchArea(fish);

        if ((fish.randomPoint - (Vector2)fish.transform.position).magnitude < 1 || !hasStarted)
        {
            fish.randomPoint = GetRandomPoints(fish.aPoint, fish.bPoint, fish.cPoint, fish.dPoint);

            hasStarted = true;
        }

        Vector2 randomPointDirection = (fish.randomPoint - (Vector2)fish.transform.position).normalized;


        if (fish.HasNeighbors())
        {
            fish.ChangeBehaviorState(BehavaiorState.GroupMove);
            ResetStartingFlag();
        }

        return randomPointDirection;
    }



    private Vector2 GetRandomPoints(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        if (Random.value < 0.5f)
        {
            return GetRandomPointInTriangle(a, b, d);
        }
        else
        {
            return GetRandomPointInTriangle(d, b, c);
        }
    }


    private Vector2 GetRandomPointInTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        float u = Random.value;
        float v = Random.value;

        if (u + v > 1f)
        {
            u = 1f - u;
            v = 1f - v;
        }

        return (1 - u - v) * a + u * b + v * c;
    }



    private void CalculateSearchArea(Fish fish)
    {
        Vector2 up = fish.transform.up;
        Vector2 forward = fish.transform.right;

        fish.aPoint = (Vector2)fish.transform.position + (up * searchAreaBottomWidth);
        fish.bPoint = (Vector2)fish.transform.position + (up * searchAreaUpperWidth) + (forward * searchAreaHeight);
        fish.cPoint = (Vector2)fish.transform.position - (up * searchAreaUpperWidth) + (forward * searchAreaHeight);
        fish.dPoint = (Vector2)fish.transform.position - (up * searchAreaBottomWidth);
    }




}
