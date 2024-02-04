using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMovement : MonoBehaviour
{
    [SerializeField] float lerpFactor = 5;
    [SerializeField] float moveSpeed;
    private GameInput gameInput;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gameInput = GameInput.Instance;
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector2 moveVector = gameInput.GetGnomeNormilizedMovementInput();
        rb2D.velocity = Vector2.Lerp(rb2D.velocity, moveVector * moveSpeed, lerpFactor * Time.deltaTime);

        if (moveVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

    }

}
