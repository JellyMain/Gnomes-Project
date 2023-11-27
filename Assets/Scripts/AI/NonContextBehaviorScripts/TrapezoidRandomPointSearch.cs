using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NonContextBehavior/TrapezoidSearch")]
public class TrapezoidRandomPointSearch : NonContextBehavior
{
    [SerializeField] float searchAreaBottomWidth = 4;
    [SerializeField] float searchAreaUpperWidth = 6;
    [SerializeField] float searchAreaHeight = 3;
    private Vector2 randomPoint;
    private bool hasStarted = false;

    private Vector2 aPoint;
    private Vector2 bPoint;
    private Vector2 cPoint;
    private Vector2 dPoint;


    public override void CalculateMove(Fish fish)
    {
        CalculateSearchArea(fish);

        if ((randomPoint - (Vector2)fish.transform.position).magnitude < 1 || !hasStarted)
        {

            randomPoint = GetRandomPoints(aPoint, bPoint, cPoint, dPoint);

            hasStarted = true;
        }


        Vector2 randomPointDirection = (randomPoint - (Vector2)fish.transform.position).normalized;

        fish.Rb2d.velocity = randomPointDirection * fish.StandartMoveSpeed;

        float angle = Mathf.Atan2(randomPointDirection.y, randomPointDirection.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        fish.transform.rotation = Quaternion.Slerp(fish.transform.rotation, rotation, fish.RotationSpeed * Time.deltaTime);

        fish.SetDrawPoints(aPoint, bPoint, cPoint, dPoint, randomPoint);
    }



    private Vector2 GetRandomPoints(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        if (Random.value < 0.5f)
        {
            return GetRandomPointInTriangle(a, b, c);
        }
        else
        {
            return GetRandomPointInTriangle(a, c, d);
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

        aPoint = (Vector2)fish.transform.position + (up * searchAreaBottomWidth);
        bPoint = (Vector2)fish.transform.position + (up * searchAreaUpperWidth) + (forward * searchAreaHeight);
        cPoint = (Vector2)fish.transform.position - (up * searchAreaUpperWidth) + (forward * searchAreaHeight);
        dPoint = (Vector2)fish.transform.position - (up * searchAreaBottomWidth);
    }




}
