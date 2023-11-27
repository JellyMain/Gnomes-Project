using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static event Action OnInventoryToggledAction;
    public static event Action OnAttackAction;

    public static event Action OnPlayerInteractedAction;


    private InputActions inputActions;


    public static GameInput Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inputActions = new InputActions();
        SetGnomeActionMap();
    }


    private void OnEnable()
    {
        SceneLoader.OnBuildingEntered += SetPlayerActionMap;
    }


    private void OnDisable()
    {
        SceneLoader.OnBuildingEntered -= SetPlayerActionMap;
    }


    private void SetGnomeActionMap()
    {
        inputActions.Gnome.Enable();
        inputActions.Gnome.ToggleInventory.performed += ToggleInvetnory;
        inputActions.Gnome.Attack.performed += Attack;
    }



    private void SetPlayerActionMap()
    {
        inputActions.Gnome.Disable();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact;
    }



    private void ToggleInvetnory(InputAction.CallbackContext context)
    {
        OnInventoryToggledAction?.Invoke();
    }


    private void Attack(InputAction.CallbackContext context)
    {
        OnAttackAction?.Invoke();
    }


    private void Interact(InputAction.CallbackContext context)
    {
        OnPlayerInteractedAction?.Invoke();
    }


    public Vector2 GetGnomeNormilizedMovementInput()
    {
        Vector2 movementInput = inputActions.Gnome.Move.ReadValue<Vector2>().normalized;
        return movementInput;
    }


    public Vector2 GetPlayerNormilizedMovementInput()
    {
        Vector2 movementInput = inputActions.Player.Move.ReadValue<Vector2>().normalized;
        return movementInput;
    }
}
