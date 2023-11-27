using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private GameInput gameInput;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        gameInput = GameInput.Instance;
        rb2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector2 moveVector = gameInput.GetPlayerNormilizedMovementInput();
        rb2D.velocity = moveVector * moveSpeed;

        if (rb2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
