using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static event Action OnInventoryToggledAction;
    public static event Action OnAttackAction;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.ToggleInventory.performed += ToggleInvetnory;
        inputActions.Player.Attack.performed += Attack;
    }


    private void ToggleInvetnory(InputAction.CallbackContext obj)
    {
        OnInventoryToggledAction?.Invoke();
    }


    private void Attack(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke();
    }


    public Vector2 GetNormilizedMovementInput()
    {
        Vector2 movementInput = inputActions.Player.Move.ReadValue<Vector2>().normalized;
        return movementInput;
    }
}
