using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector2 moveVector = gameInput.GetNormilizedMovementInput();
        rb2D.velocity = moveVector * moveSpeed;
    }

}
