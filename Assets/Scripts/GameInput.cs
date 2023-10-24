using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }





    public Vector2 GetNormilizedMovementInput()
    {
        Vector2 movementInput = inputActions.Player.Move.ReadValue<Vector2>().normalized;
        return movementInput;
    }
}
