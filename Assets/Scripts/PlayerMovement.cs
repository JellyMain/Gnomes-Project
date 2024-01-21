using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    private IInteractable interactableObject;
    private GameInput gameInput;
    private Rigidbody2D rb2D;

    public static PlayerMovement Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gameInput = GameInput.Instance;
        rb2D = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        GameInput.OnPlayerInteractedAction += TryInteract;
    }


    private void OnDisable()
    {
        GameInput.OnPlayerInteractedAction -= TryInteract;
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void TryInteract()
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactableObject = interactable;
            interactableObject.SelectItem();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactableObject.DeselectItem();
            interactableObject = null;
        }
    }


    private void Move()
    {
        Vector2 moveVector = gameInput.GetPlayerNormilizedMovementInput();
        rb2D.velocity = moveVector * moveSpeed;

        if (rb2D.velocity.x != 0)
        {
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
}
